using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Issue
{
    public class IssueDto
    {
        public Guid Id { get; init; }

        public Guid? ModuleId { get; init; }

        public Guid? LessonId { get; init; }

        public string Title { get; init; } = default!;

        public string Description { get; init; } = default!;

        public int? Position { get; init; }
    }
}
