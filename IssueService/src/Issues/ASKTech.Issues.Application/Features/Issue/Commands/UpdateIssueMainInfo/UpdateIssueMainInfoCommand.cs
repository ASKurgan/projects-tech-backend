using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Core.Abstractions;

namespace ASKTech.Issues.Application.Features.Issue.Commands.UpdateIssueMainInfo
{
    public record UpdateIssueMainInfoCommand(
    Guid IssueId,
    Guid? LessonId,
    Guid ModuleId,
    string Title,
    string Description,
    int Experience) : ICommand;
}
