using ASKTech.Core.Validation;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssuesReviews.Commands.Approve
{
    public class ApproveIssueReviewCommandValidator : AbstractValidator<ApproveIssueReviewCommand>
    {
        public ApproveIssueReviewCommandValidator()
        {
            RuleFor(c => c.IssueReviewId)
                .NotEmpty().WithError(Errors.General.ValueIsInvalid("id"));
        }
    }
}
