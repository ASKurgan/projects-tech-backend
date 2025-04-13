﻿using Amazon.S3.Model;
using Amazon.S3;
using FileService.Contracts;
//using CompleteMultipartUploadRequest = FileService.Contracts.CompleteMultipartUploadRequest;

namespace FileService.Services
{
    public class S3Provider : IS3Provider
    {
        private readonly IAmazonS3 _s3Client;
        private readonly ILogger<S3Provider> _logger;

        public S3Provider(IAmazonS3 s3Client, ILogger<S3Provider> logger)
        {
            _s3Client = s3Client;
            _logger = logger;
        }

        public async Task<List<string>> ListBucketsAsync(CancellationToken cancellationToken)
        {
            var response = await _s3Client.ListBucketsAsync(cancellationToken);

            return response.Buckets.Select(b => b.BucketName!).ToList();
        }

        public async Task<string> StartMultipartUpload(
            string fileName,
            string contentType,
            FileLocation location,
            CancellationToken cancellationToken)
        {
            await CreateBucketIfNotExists(location.BucketName, cancellationToken);

            // TODO: отменить multipart загрузку если она уже идёт
            var initiateRequest = new InitiateMultipartUploadRequest
            {
                BucketName = location.BucketName,
                Key = location.FileId,
                ContentType = contentType,
            };

            initiateRequest.Metadata.Add("file-name", fileName);

            var result = await _s3Client.InitiateMultipartUploadAsync(initiateRequest, cancellationToken);

            return result.UploadId;
        }

        public async Task<string> GenerateChunkUploadUrl(
            FileLocation location,
            string uploadId,
            int partNumber)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = location.BucketName,
                Key = location.FileId,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(60),
                PartNumber = partNumber,
                UploadId = uploadId,
                Protocol = Protocol.HTTP,
            };

            return await _s3Client.GetPreSignedURLAsync(request);
        }

        public async Task<string> GenerateUploadUrl(string fileName, FileLocation fileLocation)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = fileLocation.BucketName,
                Key = fileLocation.FileId,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(60),
                Protocol = Protocol.HTTP,
            };

            request.Metadata.Add("file-name", fileName);

            return await _s3Client.GetPreSignedURLAsync(request);
        }

        public async Task<string> CompleteMultipartUploadAsync(
            FileLocation location,
            string uploadId,
            List<(int PartNumber, string ETag)> partETags,
            CancellationToken cancellationToken)
        {
            var completeRequest = new Amazon.S3.Model.CompleteMultipartUploadRequest
            {
                BucketName = location.BucketName,
                Key = location.FileId,
                UploadId = uploadId,
                PartETags = partETags.Select(pt => new PartETag(pt.PartNumber, pt.ETag)).ToList(),
            };

            var response = await _s3Client.CompleteMultipartUploadAsync(completeRequest, cancellationToken);

            return response.Key;
        }

        public async Task AbortMultipartUploadAsync(
            FileLocation fileLocation,
            string uploadId,
            CancellationToken cancellationToken)
        {
            var abortRequest = new AbortMultipartUploadRequest
            {
                BucketName = fileLocation.BucketName,
                Key = fileLocation.FileId,
                UploadId = uploadId,
            };

            await _s3Client.AbortMultipartUploadAsync(abortRequest, cancellationToken);
        }

        public async Task<ListMultipartUploadsResponse> ListMultipartUploadAsync(
            string bucketName,
            CancellationToken cancellationToken)
        {
            var listRequest = new ListMultipartUploadsRequest { BucketName = bucketName };

            var response = await _s3Client.ListMultipartUploadsAsync(listRequest, cancellationToken);

            return response;
        }

        public async Task<string> GenerateDownloadUrlAsync(FileLocation location, int expirationHours)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = location.BucketName,
                Key = location.FileId,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddHours(expirationHours),
                Protocol = Protocol.HTTP,
            };

            return await _s3Client.GetPreSignedURLAsync(request);
        }

        public async Task<IReadOnlyList<FileUrl?>> GenerateDownloadUrlsAsync(
            IEnumerable<FileLocation> locations,
            int expirationHours)
        {
            var semaphore = new SemaphoreSlim(50);

            var tasks = locations.Select(async location =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var request = new GetPreSignedUrlRequest
                    {
                        BucketName = location.BucketName,
                        Key = location.FileId,
                        Verb = HttpVerb.GET,
                        Expires = DateTime.UtcNow.AddHours(expirationHours),
                        Protocol = Protocol.HTTP,
                    };

                    string? url = await _s3Client.GetPreSignedURLAsync(request);

                    return new FileUrl(location.FileId, url);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error with generating download url for file {fileId}", location.FileId);

                    return null;
                }
                finally
                {
                    semaphore.Release();
                }
            });

            return await Task.WhenAll(tasks);
        }

        public async Task DownloadFileAsync(FileLocation location, string tempInputPath,
            CancellationToken cancellationToken)
        {
            var request = new GetObjectRequest { BucketName = location.BucketName, Key = location.FileId, };

            using var response = await _s3Client.GetObjectAsync(request, cancellationToken);
            await using var fileStream = File.Create(tempInputPath);
            await response.ResponseStream.CopyToAsync(fileStream, cancellationToken);
        }

        public async Task UploadFileAsync(FileLocation location, Stream file, CancellationToken cancellationToken)
        {
            var request = new PutObjectRequest
            {
                BucketName = location.BucketName,
                Key = location.FileId,
                InputStream = file, // Укажите MIME-тип, если известен
            };

            await _s3Client.PutObjectAsync(request, cancellationToken);
        }

        private async Task CreateBucketIfNotExists(string bucketName, CancellationToken cancellationToken)
        {
            var response = await _s3Client.ListBucketsAsync(cancellationToken);
            if (response.Buckets.Any(b => b.BucketName.Equals(bucketName, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            var bucketRequest = new PutBucketRequest { BucketName = bucketName, UseClientRegion = true, };

            await _s3Client.PutBucketAsync(bucketRequest, cancellationToken);
        }
    }
}
