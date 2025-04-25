using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Queries.GetUserIssuesByModuleWithPagination
{
    public record GetUserIssuesByModuleWithPaginationQuery(
     Guid UserId,
     Guid ModuleId,
     string? Status,
     int Page,
     int PageSize) : IQuery;
}
