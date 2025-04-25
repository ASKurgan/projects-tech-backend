using ASKTech.Core.Abstractions;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Application.Mappers;
using ASKTech.Issues.Contracts.Lesson;
using ASKTech.Issues.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using FileService.Communication;
using FileService.Contracts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Queries.GetLessonById
{
    public class GetLessonByIdHandler : IQueryHandlerWithResult<LessonDto, GetLessonByIdQuery>
    {
        private readonly IIssuesReadDbContext _readDbContext;
        private readonly IFileService _fileService;

        public GetLessonByIdHandler(IIssuesReadDbContext readDbContext, IFileService fileService)
        {
            _readDbContext = readDbContext;
            _fileService = fileService;
        }

        public async Task<Result<LessonDto, ErrorList>> Handle(
            GetLessonByIdQuery query, CancellationToken cancellationToken = default)
        {
            // TODO: доделать получение позиции в модуле
            var lesson = await _readDbContext.ReadLessons.FirstOrDefaultAsync(l => l.Id == query.LessonId, cancellationToken);
            if (lesson is null)
                return Errors.General.NotFound(query.LessonId, "lesson").ToErrorList();

            var fileLocation = new FileLocation(lesson.Video.FileId.ToString(), lesson.Video.FileLocation);

            var videoUrlsRequest = new GetDownloadUrlsRequest([fileLocation]);

            var videoUrlsResult = await _fileService.GetDownloadUrls(videoUrlsRequest, cancellationToken);
            if (videoUrlsResult.IsFailure)
                return Errors.General.NotFound().ToErrorList();

            var videoUrls = videoUrlsResult.Value.FileUrls
                .Where(f => f != null)
                .ToDictionary(f => new Video(Guid.Parse(f!.FileId)), f => f!.Url);

            return lesson.ToDto(null, videoUrls);
        }
    }
}
