using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssuesReviews.Commands.DeleteComment
{
    public record DeleteCommentCommand(
     Guid IssueReviewId,
     Guid UserId,
     Guid CommentId) : ICommand;
}
