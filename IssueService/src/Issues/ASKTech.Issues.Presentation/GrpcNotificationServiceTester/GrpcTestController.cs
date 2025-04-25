// using Microsoft.AspNetCore.Mvc;
// using ASKTech.Framework;
// using ASKTech.Issues.Application.Features.GrpcNotificationServiceTester.Commands.AddNotificationSettings;
// using ASKTech.Issues.Application.Features.GrpcNotificationServiceTester.Commands.PushNotification;
//
// namespace ASKTech.Issues.Presentation.GrpcNotificationServiceTester;
//
// public class GrpcTestController : ApplicationController
// {
//     [HttpPost("push-notification")]
//     public async Task<ActionResult> PushNotification(
//         [FromServices] PushNotificationHandler handler,
//         [FromBody] PushNotificationCommand cmd,
//         CancellationToken cancellationToken = default)
//     {
//         var result = await handler.Handle(cmd, cancellationToken);
//         if (result.IsFailure)
//             return result.Error.ToResponse();
//
//         return Ok(result.Value);
//     }
//
//     [HttpPost("add-notification-settings")]
//     public async Task<ActionResult> AddNotificationSettings(
//         [FromServices] AddNotificationSettingsHandler handler,
//         [FromBody] AddNotificationSettingsCommand cmd,
//         CancellationToken cancellationToken = default)
//     {
//         var result = await handler.Handle(cmd, cancellationToken);
//         if (result.IsFailure)
//             return result.Error.ToResponse();
//
//         return Ok(result.Value);
//     }
// }
