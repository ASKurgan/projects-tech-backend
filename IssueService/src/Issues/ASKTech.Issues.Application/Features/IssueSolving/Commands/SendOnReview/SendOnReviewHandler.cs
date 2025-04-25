using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Issues.Domain.IssueSolving.Entities;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Core.Validation;
using ASKTech.Issues.Application.Interfaces;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.SendOnReview
{
    public class SendOnReviewHandler : ICommandHandler<SendOnReviewCommand>
    {
        private readonly IUserIssueRepository _userIssueRepository;
        private readonly ILogger<SendOnReviewHandler> _logger;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SendOnReviewCommand> _validator;

        public SendOnReviewHandler(
            IUserIssueRepository userIssueRepository,
            ILogger<SendOnReviewHandler> logger,
            IUnitOfWork unitOfWork,
            IValidator<SendOnReviewCommand> validator,
            IPublisher publisher)
        {
            _logger = logger;
            _userIssueRepository = userIssueRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _publisher = publisher;
        }

        public async Task<UnitResult<ErrorList>> Handle(
            SendOnReviewCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToList();

            (_, bool isFailure, UserIssue? userIssue, Error? error) = await _userIssueRepository
                .GetUserIssueById(UserIssueId.Create(command.UserIssueId), cancellationToken);

            if (isFailure)
            {
                _logger.LogError("UserIssue with {Id} not found", command.UserIssueId);
                return error.ToErrorList();
            }

            var pullRequestUrl = PullRequestUrl.Create(command.PullRequestUrl).Value;

            var sendOnReviewResult = userIssue.SendOnReview(pullRequestUrl);

            if (sendOnReviewResult.IsFailure)
                return sendOnReviewResult.Error.ToErrorList();

            await _publisher.PublishDomainEvents(userIssue, cancellationToken);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation("Issue with UserIssueId {UserIssueId} was created", command.UserIssueId);

            return UnitResult.Success<ErrorList>();
        }
    }
}
