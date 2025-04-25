using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Messaging
{
    public record IssueSentOnReviewEvent(Guid UserId, Guid UserIssueId, Guid IssueId);
}
