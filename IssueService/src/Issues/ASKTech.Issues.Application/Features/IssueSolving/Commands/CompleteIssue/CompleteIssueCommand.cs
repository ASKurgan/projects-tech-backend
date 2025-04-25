using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.CompleteIssue
{
    public record CompleteIssueCommand(
     Guid UserIssueId) : ICommand;
}
