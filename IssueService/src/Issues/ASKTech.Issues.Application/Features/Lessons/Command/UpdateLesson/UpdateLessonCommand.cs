using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.UpdateLesson
{
    public record UpdateLessonCommand(
     Guid LessonId,
     string Title,
     string Description,
     int Experience,
     Guid VideoId,
     Guid PreviewId,
     IEnumerable<Guid> Tags,
     IEnumerable<Guid> Issues) : ICommand;
}
