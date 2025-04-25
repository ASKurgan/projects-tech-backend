using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.UpdateLessonPosition
{
    public record UpdateLessonPositionCommand(Guid LessonId, Guid ModuleId, int Position) : ICommand;
}
