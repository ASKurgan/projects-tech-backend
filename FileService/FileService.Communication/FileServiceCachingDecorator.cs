﻿using ASKTech.Core.Caching;
using CSharpFunctionalExtensions;
using FileService.Contracts.Options;
using FileService.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Communication
{
    public class FileServiceCachingDecorator : IFileService
    {
        private readonly ICacheService _cacheService;
        private readonly IFileService _fileService;
        private readonly MinioOptions _minioOptions;

        public FileServiceCachingDecorator(
            IFileService fileService,
            ICacheService cacheService,
            IOptions<MinioOptions> options)
        {
            _fileService = fileService;
            _cacheService = cacheService;
            _minioOptions = options.Value;
        }

        public Task<Result<StartMultipartUploadResponse, ErrorList>> StartMultipartUpload(
            StartMultipartUploadRequest request, CancellationToken cancellationToken)
        {
            return _fileService.StartMultipartUpload(request, cancellationToken);
        }

        public Task<Result<CompleteMultipartUploadResponse, ErrorList>> CompleteMultipartUpload(
            CompleteMultipartUploadRequest request, CancellationToken cancellationToken)
        {
            return _fileService.CompleteMultipartUpload(request, cancellationToken);
        }

        public Task<Result<GetChunkUploadUrlResponse, ErrorList>> GetChunkUploadUrl(
            GetChunkUploadUrlRequest request,
            CancellationToken cancellationToken)
        {
            return _fileService.GetChunkUploadUrl(request, cancellationToken);
        }

        public async Task<Result<GetDownloadUrlResponse, ErrorList>> GetDownloadUrl(
            GetDownloadUrlRequest request,
            CancellationToken cancellationToken)
        {
            string cacheKey = request.FileId;

            var cachedUrl = await _cacheService.GetAsync<string>(cacheKey, cancellationToken);
            if (cachedUrl is not null)
                return new GetDownloadUrlResponse(cachedUrl);

            var urlResult = await _fileService.GetDownloadUrl(request, cancellationToken);
            if (urlResult.IsFailure)
                return urlResult.Error;

            await _cacheService.SetAsync(cacheKey, urlResult.Value.DownloadUrl,
                new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_minioOptions.UrlExpirationDays),
                }, cancellationToken);

            return urlResult.Value;
        }

        public async Task<Result<GetDownloadUrlsResponse, ErrorList>> GetDownloadUrls(
            GetDownloadUrlsRequest request,
            CancellationToken cancellationToken)
        {
            var fileUrls = new List<FileUrl>();
            var uncachedFileIds = new List<FileLocation>();

            foreach (var location in request.Locations)
            {
                string cacheKey = location.FileId;
                var cachedUrl = await _cacheService.GetAsync<string>(cacheKey, cancellationToken);

                if (cachedUrl is not null)
                    fileUrls.Add(new FileUrl(cacheKey, cachedUrl));
                else
                    uncachedFileIds.Add(new FileLocation(location.FileId, location.BucketName));
            }

            if (uncachedFileIds.Any())
            {
                var uncachedRequest = new GetDownloadUrlsRequest(uncachedFileIds);
                var urlResult = await _fileService.GetDownloadUrls(uncachedRequest, cancellationToken);

                if (urlResult.IsFailure)
                    return urlResult.Error;

                foreach (var fileUrl in urlResult.Value.FileUrls.Where(f => f is not null))
                {
                    string cacheKey = fileUrl.FileId;
                    _cacheService.SetAsync(cacheKey, fileUrl.Url,
                        new DistributedCacheEntryOptions()
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_minioOptions.UrlExpirationDays),
                        }, cancellationToken).Wait();

                    fileUrls.Add(fileUrl);
                }
            }

            return new GetDownloadUrlsResponse(fileUrls);
        }
    }
}
