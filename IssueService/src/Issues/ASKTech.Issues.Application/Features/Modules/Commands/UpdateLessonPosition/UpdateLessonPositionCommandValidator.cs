﻿using ASKTech.Core.Validation;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.UpdateLessonPosition
{
    public class UpdateLessonPositionCommandValidator : AbstractValidator<UpdateLessonPositionCommand>
    {
        public UpdateLessonPositionCommandValidator()
        {
            RuleFor(a => a.LessonId)
                .NotNull().WithError(Errors.General.ValueIsRequired("LessonId"));

            RuleFor(a => a.ModuleId)
                .NotNull().WithError(Errors.General.ValueIsRequired("ModuleId"));

            RuleFor(a => a.Position)
                .NotNull().WithError(Errors.General.ValueIsRequired("Position"));
        }
    }
}
