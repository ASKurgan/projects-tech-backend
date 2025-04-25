using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.IssueReview
{
    public record CreateIssueReviewRequest(string PullRequestUrl);
}
