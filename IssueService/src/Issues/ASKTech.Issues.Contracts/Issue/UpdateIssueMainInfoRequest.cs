using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Issue
{
    public record UpdateIssueMainInfoRequest(
    Guid LessonId,
    Guid ModuleId,
    string Title,
    string Description,
    int Experience);
}
