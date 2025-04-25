﻿using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Core.Validation;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.Module.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.UpdateLessonPosition
{
    public class UpdateLessonPositionHandler : ICommandHandler<Guid, UpdateLessonPositionCommand>
    {
        private readonly IValidator<UpdateLessonPositionCommand> _validator;
        private readonly IModulesRepository _modulesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateLessonPositionHandler> _logger;

        public UpdateLessonPositionHandler(
            IValidator<UpdateLessonPositionCommand> validator,
            IModulesRepository modulesRepository,
            IUnitOfWork unitOfWork,
            ILogger<UpdateLessonPositionHandler> logger)
        {
            _validator = validator;
            _modulesRepository = modulesRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateLessonPositionCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToList();

            var moduleResult = await _modulesRepository.GetById(command.ModuleId, cancellationToken);
            if (moduleResult.IsFailure)
                return moduleResult.Error.ToErrorList();

            var lessonResult = moduleResult.Value.LessonsPosition.FirstOrDefault(l => l.LessonId == command.LessonId);
            if (lessonResult is null)
                return Errors.General.NotFound(command.LessonId, "lesson").ToErrorList();

            var newPosition = Position.Create(command.Position).Value;

            var result = moduleResult.Value.MoveLesson(lessonResult, newPosition);
            if (result.IsFailure)
                return result.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation(
                "Lesson position was updated with id {lessonId}",
                command.LessonId);

            return lessonResult.LessonId.Value;
        }
    }
}
