using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.IssueSolving.DomainEvents
{
    public record IssueSentOnReviewEvent(
     UserIssueId UserIssueId,
     Guid UserId,
     PullRequestUrl PullRequestUrl) : IDomainEvent;
}
