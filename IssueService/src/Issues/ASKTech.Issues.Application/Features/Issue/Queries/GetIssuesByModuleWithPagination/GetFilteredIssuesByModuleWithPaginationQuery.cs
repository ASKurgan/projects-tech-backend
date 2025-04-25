using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Issue.Queries.GetIssuesByModuleWithPagination
{
    public record GetFilteredIssuesByModuleWithPaginationQuery(
      Guid ModuleId,
      string? Title,
      string? SortBy,
      string? SortDirection,
      int Page,
      int PageSize) : IQuery;
}
