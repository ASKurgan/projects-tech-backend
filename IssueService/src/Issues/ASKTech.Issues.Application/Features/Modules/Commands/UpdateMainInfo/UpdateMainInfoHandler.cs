﻿using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Core.Validation;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.UpdateMainInfo
{
    public class UpdateMainInfoHandler : ICommandHandler<Guid, UpdateMainInfoCommand>
    {
        private readonly IModulesRepository _modulesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateMainInfoCommand> _validator;
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public UpdateMainInfoHandler(
            IModulesRepository modulesRepository,
            IUnitOfWork unitOfWork,
            IValidator<UpdateMainInfoCommand> validator,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _modulesRepository = modulesRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateMainInfoCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToList();

            var moduleResult = await _modulesRepository.GetById(command.ModuleId, cancellationToken);
            if (moduleResult.IsFailure)
                return moduleResult.Error.ToErrorList();

            var title = Title.Create(command.Title).Value;
            var description = Description.Create(command.Description).Value;

            moduleResult.Value.UpdateMainInfo(title, description);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation(
                "Updated module {title}, {description} with id {moduleId}",
                title,
                description,
                command.ModuleId);

            return moduleResult.Value.Id.Value;
        }
    }
}
