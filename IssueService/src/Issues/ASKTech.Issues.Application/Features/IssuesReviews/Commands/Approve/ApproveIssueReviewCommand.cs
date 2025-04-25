using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssuesReviews.Commands.Approve
{
    public record ApproveIssueReviewCommand(
     Guid IssueReviewId,
     Guid ReviewerId) : ICommand;
}
