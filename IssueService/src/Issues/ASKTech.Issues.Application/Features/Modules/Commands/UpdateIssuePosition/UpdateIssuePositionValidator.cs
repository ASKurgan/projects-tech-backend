using ASKTech.Core.Validation;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.UpdateIssuePosition
{
    public class UpdateIssuePositionValidator : AbstractValidator<UpdateIssuePositionCommand>
    {
        public UpdateIssuePositionValidator()
        {
            RuleFor(u => u.ModuleId)
                .NotEmpty().WithError(Errors.General.ValueIsRequired());

            RuleFor(u => u.IssueId)
                .NotEmpty().WithError(Errors.General.ValueIsRequired());

            RuleFor(u => u.NewPosition)
                .GreaterThanOrEqualTo(1).LessThanOrEqualTo(1000);
        }
    }
}
