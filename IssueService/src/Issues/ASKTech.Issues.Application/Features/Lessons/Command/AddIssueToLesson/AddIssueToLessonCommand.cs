using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.AddIssueToLesson
{
    public record AddIssueToLessonCommand(Guid LessonId, Guid IssueId) : ICommand;
}
