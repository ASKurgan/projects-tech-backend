using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.RemoveTagFromLesson
{
    public record RemoveTagFromLessonCommand(Guid LessonId, Guid TagId) : ICommand;
}
