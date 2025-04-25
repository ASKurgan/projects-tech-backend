using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Issues.Domain.ValueObjects.Ids;
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
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Core.Validation;
using ASKTech.Issues.Domain.Module;

namespace ASKTech.Issues.Application.Features.Modules.Commands.Create
{
    public class CreateModuleHandler : ICommandHandler<Guid, CreateModuleCommand>
    {
        private readonly IModulesRepository _modulesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateModuleCommand> _validator;
        private readonly ILogger<CreateModuleHandler> _logger;

        public CreateModuleHandler(
            IModulesRepository modulesRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateModuleCommand> validator,
            ILogger<CreateModuleHandler> logger)
        {
            _modulesRepository = modulesRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            CreateModuleCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToList();

            var title = Title.Create(command.Title).Value;
            var description = Description.Create(command.Description).Value;

            var module = await _modulesRepository.GetByTitle(title, cancellationToken);

            if (module.IsSuccess)
                return Errors.General.AlreadyExist().ToErrorList();

            var moduleId = ModuleId.NewModuleId();

            var moduleToCreate = new Module(moduleId, title, description);

            await using var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            await _modulesRepository.Add(moduleToCreate, cancellationToken);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("Created module {title} with id {moduleId}", title, moduleId);

            await transaction.CommitAsync(cancellationToken);

            return (Guid)moduleToCreate.Id;
        }
    }
}
