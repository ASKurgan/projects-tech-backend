using ASKTech.Core.Validation;
using ASKTech.Issues.Domain.ValueObjects;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.UpdateMainInfo
{
    public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
    {
        public UpdateMainInfoCommandValidator()
        {
            RuleFor(r => r.ModuleId).NotEmpty().WithError(Errors.General.ValueIsRequired());

            RuleFor(r => r.Title)
                .MustBeValueObject(Title.Create);

            RuleFor(r => r.Description)
                .MustBeValueObject(Description.Create);
        }
    }
}
