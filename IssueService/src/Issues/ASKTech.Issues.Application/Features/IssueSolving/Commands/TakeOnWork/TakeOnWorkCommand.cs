using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.TakeOnWork
{
    public record TakeOnWorkCommand(Guid UserId, Guid IssueId, Guid ModuleId) : ICommand;
}
