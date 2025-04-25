﻿using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Issues.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.AddIssueToLesson
{
    public class AddIssueToLessonHandler : ICommandHandler<AddIssueToLessonCommand>
    {
        private readonly IIssuesReadDbContext _readDbContext;
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddIssueToLessonHandler> _logger;

        public AddIssueToLessonHandler(
            IIssuesReadDbContext readDbContext,
            ILessonsRepository lessonsRepository,
            IUnitOfWork unitOfWork,
            ILogger<AddIssueToLessonHandler> logger)
        {
            _readDbContext = readDbContext;
            _lessonsRepository = lessonsRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UnitResult<ErrorList>> Handle(
            AddIssueToLessonCommand command, CancellationToken cancellationToken = default)
        {
            var lesson = await _lessonsRepository.GetById(command.LessonId, cancellationToken);
            if (lesson.IsFailure)
                return Errors.General.NotFound(command.LessonId, "lesson").ToErrorList();

            var isIssueExists
                = await _readDbContext.ReadIssues.FirstOrDefaultAsync(i => i.Id == command.IssueId, cancellationToken);
            if (isIssueExists is null)
                return Errors.General.NotFound(command.IssueId, "issue").ToErrorList();

            var result = lesson.Value.AddIssue(command.IssueId);
            if (result.IsFailure)
                return result.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.Log(LogLevel.Information, "Added new issue with {IssueId} to {LessonId}", command.IssueId, command.LessonId);

            return UnitResult.Success<ErrorList>();
        }
    }
}
