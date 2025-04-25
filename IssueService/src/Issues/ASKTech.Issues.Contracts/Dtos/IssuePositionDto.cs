using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Dtos
{
    public record IssuePositionDto(Guid IssueId, int Position);
}
