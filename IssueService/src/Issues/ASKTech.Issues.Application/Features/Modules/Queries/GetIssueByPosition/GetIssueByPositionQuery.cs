using ASKTech.Core.Abstractions;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Queries.GetIssueByPosition
{
    public record GetIssueByPositionQuery(ModuleId ModuleId, int Position) : IQuery;
}
