using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.RemoveIssueFromLesson
{
    public record RemoveIssueFromLessonCommand(Guid LessonId, Guid IssueId) : ICommand;
}
