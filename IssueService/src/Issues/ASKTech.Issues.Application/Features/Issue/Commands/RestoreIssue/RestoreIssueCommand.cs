using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Core.Abstractions;

namespace ASKTech.Issues.Application.Features.Issue.Commands.RestoreIssue
{
    public record RestoreIssueCommand(Guid IssueId) : ICommand;
}
