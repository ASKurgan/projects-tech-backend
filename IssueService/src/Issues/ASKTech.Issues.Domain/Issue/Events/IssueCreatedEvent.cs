using ASKTech.Issues.Domain.ValueObjects.Ids;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.Issue.Events
{
    public record IssueCreatedEvent(IssueId IssueId, ModuleId ModuleId) : IDomainEvent;
}
