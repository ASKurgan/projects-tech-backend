using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.StopWorking
{
    public record StopWorkingCommand(Guid UserIssueId, Guid UserId) : ICommand;
}
