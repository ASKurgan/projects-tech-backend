using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.IssueSolving
{
    public record GetUserIssuesByModuleWithPaginationRequest(
     Guid UserId,
     Guid ModuleId,
     string? Status,
     int Page,
     int PageSize);
}
