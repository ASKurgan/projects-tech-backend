using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Commands.UpdateMainInfo
{
    public record UpdateMainInfoCommand(Guid ModuleId, string Title, string Description) : ICommand;
}
