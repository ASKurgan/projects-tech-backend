using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Core.Abstractions;
using System.Windows.Input;
using ICommand = ASKTech.Core.Abstractions.ICommand;
using ASKTech.Issues.Contracts.Dtos;

namespace ASKTech.Issues.Application.Features.GrpcNotificationServiceTester.Commands.PushNotification
{
    public record PushNotificationCommand(
    MessageDto Message,
    Guid[] UserIds,
    Guid[] RoleIds,
    string Type,
    string Data) : ICommand;
}
