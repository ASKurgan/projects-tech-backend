﻿using ASKTech.Core.Validation;
using ASKTech.Issues.Domain.ValueObjects;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Issue.Commands.UpdateIssueMainInfo
{
    public class UpdateIssueMainInfoValidator : AbstractValidator<UpdateIssueMainInfoCommand>
    {
        public UpdateIssueMainInfoValidator()
        {
            RuleFor(u => u.IssueId)
                .NotEmpty().WithError(Errors.General.ValueIsRequired());

            RuleFor(u => u.Title)
                .MustBeValueObject(Title.Create);

            RuleFor(u => u.Description)
                .MustBeValueObject(Description.Create);

            RuleFor(u => u.Experience)
                .GreaterThanOrEqualTo(1).LessThanOrEqualTo(1000);
        }
    }
}
