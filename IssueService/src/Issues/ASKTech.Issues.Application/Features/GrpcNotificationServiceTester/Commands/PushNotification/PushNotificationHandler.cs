// using CSharpFunctionalExtensions;
// using ASKTech.Core.Abstractions;
// using ASKTech.Issues.Application.Interfaces;
// using ASKTech.SharedKernel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.GrpcNotificationServiceTester.Commands.PushNotification
{
    // public class PushNotificationHandler : ICommandHandler<Guid, PushNotificationCommand>
    // {
    //     private readonly IGrpcNotificationServiceClient _grpcClient;
    //
    //     public PushNotificationHandler(IGrpcNotificationServiceClient grpcClient)
    //     {
    //         _grpcClient = grpcClient;
    //     }
    //     public async Task<Result<Guid, ErrorList>> Handle(PushNotificationCommand command, CancellationToken cancellationToken = default)
    //     {
    //         var result = await _grpcClient.PushNotificationAsync(command, cancellationToken);
    //         return result;
    //     }
    // }
}
