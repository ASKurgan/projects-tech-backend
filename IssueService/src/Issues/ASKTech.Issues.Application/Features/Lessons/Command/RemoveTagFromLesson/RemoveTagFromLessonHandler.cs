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

namespace ASKTech.Issues.Application.Features.Lessons.Command.RemoveTagFromLesson
{
    public class RemoveTagFromLessonHandler : ICommandHandler<RemoveTagFromLessonCommand>
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveTagFromLessonHandler> _logger;

        public RemoveTagFromLessonHandler(
            ILessonsRepository lessonsRepository,
            IUnitOfWork unitOfWork,
            ILogger<RemoveTagFromLessonHandler> logger)
        {
            _lessonsRepository = lessonsRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UnitResult<ErrorList>> Handle(
            RemoveTagFromLessonCommand command, CancellationToken cancellationToken = default)
        {
            var lesson = await _lessonsRepository.GetById(command.LessonId, cancellationToken);
            if (lesson.IsFailure)
                return Errors.General.NotFound(command.LessonId, "lesson").ToErrorList();

            var result = lesson.Value.RemoveTag(command.TagId);
            if (result.IsFailure)
                return result.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.Log(LogLevel.Information, "Remove tag with {TagId} from {LessonId}", command.TagId, command.LessonId);

            return UnitResult.Success<ErrorList>();
        }
    }
}
