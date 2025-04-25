using ASKTech.Core.Database;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.IssueSolving.DomainEvents;
using ASKTech.Issues.Domain.IssuesReviews;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssuesReviews.EventHandlers
{
    public class IssueReviewCreation : INotificationHandler<IssueSentOnReviewEvent>
    {
        private readonly IIssuesReviewRepository _issuesReviewRepository;
        private readonly ILogger<IssueReviewCreation> _logger;

        public IssueReviewCreation(
            IIssuesReviewRepository issuesReviewRepository,
            ILogger<IssueReviewCreation> logger,
            IUnitOfWork unitOfWork)
        {
            _issuesReviewRepository = issuesReviewRepository;
            _logger = logger;
        }

        public async Task Handle(IssueSentOnReviewEvent domainEvent, CancellationToken cancellationToken)
        {
            var issueReviewResult = new IssueReview(
                IssueReviewId.NewIssueReviewId(),
                domainEvent.UserIssueId,
                domainEvent.UserId,
                domainEvent.PullRequestUrl);

            await _issuesReviewRepository.Add(issueReviewResult, cancellationToken);

            _logger.LogInformation("IssueReview {IssueReviewId} was created", issueReviewResult.Id);
        }
    }
}
