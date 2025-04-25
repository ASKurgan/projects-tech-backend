using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Queries.GetModules
{
    public record GetModulesQuery(string? Title, int Page, int PageSize) : IQuery;
}
