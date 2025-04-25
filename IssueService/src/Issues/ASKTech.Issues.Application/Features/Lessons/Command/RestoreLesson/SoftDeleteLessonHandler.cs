using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Issues.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.RestoreLesson
{
    public class RestoreLessonHandler : ICommandHandler<RestoreLessonCommand>
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RestoreLessonHandler> _logger;

        public RestoreLessonHandler(
            ILessonsRepository lessonsRepository,
            IUnitOfWork unitOfWork,
            ILogger<RestoreLessonHandler> logger)
        {
            _lessonsRepository = lessonsRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UnitResult<ErrorList>> Handle(
            RestoreLessonCommand command, CancellationToken cancellationToken = default)
        {
            var lesson = await _lessonsRepository.GetById(command.LessonId, cancellationToken);
            if (lesson.IsFailure)
                return Errors.General.NotFound(command.LessonId, "lesson").ToErrorList();

            lesson.Value.Restore();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.Log(LogLevel.Information, "Lesson with id {LessonId} restored", command.LessonId);

            return UnitResult.Success<ErrorList>();
        }
    }
}
