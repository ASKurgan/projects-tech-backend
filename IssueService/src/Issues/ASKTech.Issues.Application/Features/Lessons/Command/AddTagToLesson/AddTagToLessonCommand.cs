using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.AddTagToLesson
{
    public record AddTagToLessonCommand(Guid LessonId, Guid TagId) : ICommand;
}
