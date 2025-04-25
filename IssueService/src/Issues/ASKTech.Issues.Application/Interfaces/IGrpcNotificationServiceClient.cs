using ASKTech.Issues.Application.Features.GrpcNotificationServiceTester.Commands.AddNotificationSettings;
using ASKTech.Issues.Application.Features.GrpcNotificationServiceTester.Commands.PushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Interfaces
{
    public interface IGrpcNotificationServiceClient
    {
        Task<Guid> AddNotificationSettingsAsync(AddNotificationSettingsCommand command, CancellationToken cancellationToken = default);

        Task<Guid> PushNotificationAsync(PushNotificationCommand command, CancellationToken cancellationToken = default);
    }
}
