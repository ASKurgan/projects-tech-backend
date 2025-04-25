using ASKTech.Issues.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Module
{
    public record ModuleDto(
     Guid Id,
     string Title,
     string Description,
     IReadOnlyList<IssuePositionDto> IssuesPositions,
     IReadOnlyList<LessonPositionDto> LessonPositions);
}
