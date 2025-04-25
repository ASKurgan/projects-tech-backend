using ASKTech.Core.Validation;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.SendForRevision
{
    public class SendUserIssueForRevisionCommandValidator : AbstractValidator<SendUserIssueForRevisionCommand>
    {
        public SendUserIssueForRevisionCommandValidator()
        {
            RuleFor(c => c.UserIssueId)
                .NotEmpty().WithError(Errors.General.ValueIsInvalid("id"));
        }
    }
}
