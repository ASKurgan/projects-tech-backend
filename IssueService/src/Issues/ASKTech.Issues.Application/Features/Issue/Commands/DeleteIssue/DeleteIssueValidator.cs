using ASKTech.Core.Validation;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Issue.Commands.DeleteIssue
{
    public class DeleteIssueValidator : AbstractValidator<DeleteIssueCommand>
    {
        public DeleteIssueValidator()
        {
            RuleFor(u => u.IssueId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        }
    }
}
