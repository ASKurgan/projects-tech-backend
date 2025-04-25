using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Core.Validation;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.SendForRevision
{
    public class SendUserIssueForRevisionHandler : ICommandHandler<Guid, SendUserIssueForRevisionCommand>
    {
        private readonly IUserIssueRepository _userIssueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SendUserIssueForRevisionCommand> _validator;
        private readonly ILogger<SendUserIssueForRevisionHandler> _logger;

        public SendUserIssueForRevisionHandler(
            IUserIssueRepository userIssueRepository,
            IUnitOfWork unitOfWork,
            IValidator<SendUserIssueForRevisionCommand> validator,
            ILogger<SendUserIssueForRevisionHandler> logger)
        {
            _userIssueRepository = userIssueRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            SendUserIssueForRevisionCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToList();

            var userIssueResult = await _userIssueRepository
                .GetUserIssueById(UserIssueId.Create(command.UserIssueId), cancellationToken);

            if (userIssueResult.IsFailure)
                return userIssueResult.Error.ToErrorList();

            var sendForRevisionResult = userIssueResult.Value.SendForRevision();

            if (sendForRevisionResult.IsFailure)
                return sendForRevisionResult.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation(
                "User Issue {userIssue} is sent for review",
                userIssueResult.Value.Id.Value);

            return userIssueResult.Value.Id.Value;
        }
    }
}
