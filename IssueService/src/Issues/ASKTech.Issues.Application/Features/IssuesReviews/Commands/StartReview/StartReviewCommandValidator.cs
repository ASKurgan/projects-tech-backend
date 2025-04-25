using ASKTech.Core.Validation;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssuesReviews.Commands.StartReview
{
    public class StartReviewCommandValidator : AbstractValidator<StartReviewCommand>
    {
        public StartReviewCommandValidator()
        {
            RuleFor(c => c.IssueReviewId)
                .NotEmpty().WithError(Errors.General.ValueIsInvalid("id"));
        }
    }
}
