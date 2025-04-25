using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Presentation.Lessons
{
    public record GetLessonsRequest(int Page, int PageSize, Guid ModuleId, string? Search);
}
