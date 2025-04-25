using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Queries.GetLessonsByModuleWithPagination
{
    public record GetLessonsWithPaginationQuery(int Page, int PageSize, Guid ModuleId, string? Search) : IQuery;
}
