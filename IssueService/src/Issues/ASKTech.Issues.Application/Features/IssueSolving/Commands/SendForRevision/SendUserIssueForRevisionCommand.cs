using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.SendForRevision
{
    public record SendUserIssueForRevisionCommand(
     Guid UserIssueId) : ICommand;
}
