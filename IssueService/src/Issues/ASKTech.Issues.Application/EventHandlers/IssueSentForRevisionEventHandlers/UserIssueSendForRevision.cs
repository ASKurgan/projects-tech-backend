using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.IssuesReviews.Events;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.EventHandlers.IssueSentForRevisionEventHandlers
{
    public class UserIssueSendForRevision : INotificationHandler<IssueSentForRevisionEvent>
    {
        private readonly IUserIssueRepository _userIssueRepository;

        public UserIssueSendForRevision(IUserIssueRepository userIssueRepository)
        {
            _userIssueRepository = userIssueRepository;
        }

        public async Task Handle(IssueSentForRevisionEvent domainEvent, CancellationToken cancellationToken)
        {
            var userIssueResult = await _userIssueRepository
                .GetUserIssueById(domainEvent.UserIssueId, cancellationToken);

            if (userIssueResult.IsFailure)
                throw new Exception(userIssueResult.Error.Message);

            var userIssue = userIssueResult.Value;

            var sendForRevisionResult = userIssue.SendForRevision();

            if (sendForRevisionResult.IsFailure)
                throw new Exception(sendForRevisionResult.Error.Message);
        }
    }
}
