using ASKTech.Core.Validation;
using ASKTech.Issues.Domain.IssuesReviews.ValueObjects;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssuesReviews.Commands.AddComment
{
    public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator()
        {
            RuleFor(c => c.IssueReviewId)
                .NotEmpty().WithError(Errors.General.ValueIsInvalid("id"));

            RuleFor(c => c.Message)
                .MustBeValueObject(Message.Create);
        }
    }
}
