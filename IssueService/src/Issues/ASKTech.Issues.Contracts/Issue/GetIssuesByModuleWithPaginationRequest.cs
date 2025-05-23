﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Issue
{
    public record GetIssuesByModuleWithPaginationRequest(
     string? Title,
     string? SortBy,
     string? SortDirection,
     int Page,
     int PageSize);
}
