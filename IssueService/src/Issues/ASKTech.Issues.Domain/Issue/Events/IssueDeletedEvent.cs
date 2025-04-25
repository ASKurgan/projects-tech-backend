using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.Issue.Events
{
    public record IssueDeletedEvent(Guid ModuleId, Guid IssueId) : IDomainEvent;
}
