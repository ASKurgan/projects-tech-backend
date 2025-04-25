using ASKTech.Core.Abstractions;
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

namespace ASKTech.Issues.Application.Features.Modules.Commands.UpdateIssuePosition
{
    public class UpdateIssuePositionHandler : ICommandHandler<Guid, UpdateIssuePositionCommand>
    {
        private readonly IModulesRepository _modulesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateIssuePositionCommand> _validator;
        private readonly ILogger<UpdateIssuePositionHandler> _logger;

        public UpdateIssuePositionHandler(
            IModulesRepository modulesRepository,
            IUnitOfWork unitOfWork,
            IValidator<UpdateIssuePositionCommand> validator,
            ILogger<UpdateIssuePositionHandler> logger)
        {
            _modulesRepository = modulesRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateIssuePositionCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToList();

            var moduleResult = await _modulesRepository.GetById(command.ModuleId, cancellationToken);
            if (moduleResult.IsFailure)
                return moduleResult.Error.ToErrorList();

            var issueResult = moduleResult.Value.IssuesPosition.FirstOrDefault(i => i.IssueId == command.IssueId);
            if (issueResult == null)
                return Errors.General.NotFound(command.IssueId).ToErrorList();

            var newPosition = Position.Create(command.NewPosition).Value;

            var result = moduleResult.Value.MoveIssue(issueResult, newPosition);
            if (result.IsFailure)
                return moduleResult.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation(
                "Changed issue position with id {issueId} in module {moduleId}",
                command.IssueId,
                command.ModuleId);

            return issueResult.IssueId.Value;
        }
    }
}
