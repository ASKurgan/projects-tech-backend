using ASKTech.Core.Validation;
using ASKTech.Issues.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.Create
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(c => c.Title)
                .MustBeValueObject(Title.Create);

            RuleFor(c => c.Description)
                .MustBeValueObject(Description.Create);
        }
    }
}
