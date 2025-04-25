using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.UpdateIssuePosition
{
    public record UpdateIssuePositionCommand(Guid ModuleId, Guid IssueId, int NewPosition) : ICommand;
}
