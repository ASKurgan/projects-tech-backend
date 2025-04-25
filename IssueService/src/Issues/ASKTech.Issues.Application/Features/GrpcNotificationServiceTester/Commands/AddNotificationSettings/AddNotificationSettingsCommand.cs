using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Core.Abstractions;
using System.Windows.Input;
using ICommand = ASKTech.Core.Abstractions.ICommand;

namespace ASKTech.Issues.Application.Features.GrpcNotificationServiceTester.Commands.AddNotificationSettings
{
    public record AddNotificationSettingsCommand(
     Guid UserId,
     string Email,
     string? WebEndpoint) : ICommand;
}
