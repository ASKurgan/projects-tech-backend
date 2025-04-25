using ASKTech.Core.Validation;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.CompleteIssue
{
    public class CompleteIssueCommandValidator : AbstractValidator<CompleteIssueCommand>
    {
        public CompleteIssueCommandValidator()
        {
            RuleFor(c => c.UserIssueId)
                .NotEmpty().WithError(Errors.General.ValueIsInvalid("id"));
        }
    }
}
