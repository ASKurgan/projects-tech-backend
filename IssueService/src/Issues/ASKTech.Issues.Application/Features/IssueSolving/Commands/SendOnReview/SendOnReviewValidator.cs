using ASKTech.Core.Validation;
using ASKTech.Issues.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.SendOnReview
{
    public class SendOnReviewValidator : AbstractValidator<SendOnReviewCommand>
    {
        public SendOnReviewValidator()
        {
            RuleFor(s => s.PullRequestUrl).MustBeValueObject(PullRequestUrl.Create);
        }
    }
}
