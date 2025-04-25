using ASKTech.Core.Validation;
using ASKTech.Issues.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Issue.Commands.AddIssue
{
    public class CreateIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueCommandValidator()
        {
            RuleFor(c => c.Title)
                .MustBeValueObject(Title.Create);

            RuleFor(c => c.Description)
                .MustBeValueObject(Description.Create);
        }
    }
}
