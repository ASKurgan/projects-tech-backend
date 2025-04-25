using ASKTech.Core.Validation;
using FluentValidation;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Queries.GetLessonsByModuleWithPagination
{
    public class GetLessonsByModuleWithPaginationValidator : AbstractValidator<GetLessonsWithPaginationQuery>
    {
        public GetLessonsByModuleWithPaginationValidator()
        {
            RuleFor(v => v.Page)
                .GreaterThanOrEqualTo(1)
                .WithError(Errors.General.ValueIsInvalid("Page"));

            RuleFor(v => v.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithError(Errors.General.ValueIsInvalid("PageSize"));
        }
    }
}
