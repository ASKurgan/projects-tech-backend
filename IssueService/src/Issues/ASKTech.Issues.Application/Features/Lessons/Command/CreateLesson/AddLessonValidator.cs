using ASKTech.Core.Validation;
using ASKTech.Issues.Domain.Issue.ValueObjects;
using ASKTech.Issues.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.CreateLesson
{
    public class AddLessonValidator : AbstractValidator<CreateLessonCommand>
    {
        public AddLessonValidator()
        {
            RuleFor(a => a.Title)
                .MustBeValueObject(Title.Create);

            RuleFor(a => a.Description)
                .MustBeValueObject(Description.Create);

            RuleFor(a => a.Experience)
                .MustBeValueObject(Experience.Create);
        }
    }
}
