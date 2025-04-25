using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Issue
{
    public record AddIssueRequest(
     [Required] Guid ModuleId,
     Guid? LessonId,
     string Title,
     string Description,
     int Experience);
}
