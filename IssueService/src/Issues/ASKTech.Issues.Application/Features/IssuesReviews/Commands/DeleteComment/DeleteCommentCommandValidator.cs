using ASKTech.Core.Validation;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssuesReviews.Commands.DeleteComment
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(c => c.IssueReviewId)
                .NotEmpty().WithError(Errors.General.ValueIsInvalid("id"));

            RuleFor(c => c.CommentId)
                .NotEmpty().WithError(Errors.General.ValueIsInvalid("comment_id"));
        }
    }
}
