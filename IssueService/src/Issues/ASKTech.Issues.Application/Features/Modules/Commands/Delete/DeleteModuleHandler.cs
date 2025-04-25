using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Core.Validation;
using ASKTech.Issues.Application.Interfaces;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.Delete
{
    public class DeleteModuleHandler : ICommandHandler<Guid, DeleteModuleCommand>
    {
        private readonly IModulesRepository _modulesRepository;
        private readonly ILogger<DeleteModuleHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DeleteModuleCommand> _validator;

        public DeleteModuleHandler(
            IModulesRepository modulesRepository,
            IUnitOfWork unitOfWork,
            IValidator<DeleteModuleCommand> validator,
            ILogger<DeleteModuleHandler> logger)
        {
            _modulesRepository = modulesRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            DeleteModuleCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToList();

            var moduleResult = await _modulesRepository.GetById(command.ModuleId, cancellationToken);
            if (moduleResult.IsFailure)
                return moduleResult.Error.ToErrorList();

            moduleResult.Value.SoftDelete();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("Updated deleted with id {moduleId}", command.ModuleId);

            return moduleResult.Value.Id.Value;
        }
    }
}
