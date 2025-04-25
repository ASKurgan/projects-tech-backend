using ASKTech.Issues.Application.Features.Lessons.Command.AddIssueToLesson;
using ASKTech.Issues.Application.Features.Lessons.Command.AddTagToLesson;
using ASKTech.Issues.Application.Features.Lessons.Command.CreateLesson;
using ASKTech.Issues.Application.Features.Lessons.Command.RemoveIssueFromLesson;
using ASKTech.Issues.Application.Features.Lessons.Command.RestoreLesson;
using ASKTech.Issues.Application.Features.Lessons.Command.SoftDeleteLesson;
using ASKTech.Issues.Application.Features.Lessons.Command.StartUploadVideo;
using ASKTech.Issues.Application.Features.Lessons.Command.UpdateLesson;
using ASKTech.Issues.Application.Features.Lessons.Queries.GetLessonById;
using ASKTech.Issues.Application.Features.Lessons.Queries.GetLessonsByModuleWithPagination;
using ASKTech.Issues.Application.Features.Modules.Commands.UpdateLessonPosition;
using ASKTech.Issues.Contracts.Lesson;
using Microsoft.AspNetCore.Mvc;

using ASKTech.Framework;
using ASKTech.Framework.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileService.Contracts;

namespace ASKTech.Issues.Presentation.Lessons
{
    public class LessonsController : ApplicationController
    {
        [HttpGet]
        [Permission(Permissions.Lessons.READ_LESSON)]
        public async Task<IActionResult> GetLessons(
            [FromQuery] GetLessonsRequest request,
            [FromServices] GetLessonsByModuleWithPaginationHandler byModuleWithPaginationHandler,
            CancellationToken cancellationToken)
        {
            var result = await byModuleWithPaginationHandler.Handle(
                new GetLessonsWithPaginationQuery(request.Page, request.PageSize, request.ModuleId, request.Search),
                cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpGet("{lessonId:guid}")]
        [Permission(Permissions.Lessons.READ_LESSON)]
        public async Task<IActionResult> GetLessonById(
            [FromRoute] Guid lessonId,
            [FromServices] GetLessonByIdHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new GetLessonByIdQuery(lessonId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost("video")]
        [Permission(Permissions.Lessons.UPDATE_LESSON)]
        public async Task<ActionResult<StartMultipartUploadResponse>> StartUploadVideo(
            [FromServices] StartUploadVideoHandler handler,
            [FromBody] FileMetadataRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new StartUploadVideoCommand(
                request.FileName,
                request.ContentType,
                request.Size);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost]
        [Permission(Permissions.Lessons.CREATE_LESSON)]
        public async Task<IActionResult> CreateLesson(
            [FromBody] CreateLessonRequest request,
            [FromServices] CreateLessonHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new CreateLessonCommand(
                request.ModuleId,
                request.Title,
                request.Description,
                request.Experience,
                request.Tags,
                request.Issues,
                request.MultipartRequest);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut]
        [Permission(Permissions.Lessons.UPDATE_LESSON)]
        public async Task<IActionResult> UpdateLesson(
            [FromBody] UpdateLessonRequest request,
            [FromServices] UpdateLessonHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new UpdateLessonCommand(
                request.LessonId,
                request.Title,
                request.Description,
                request.Experience,
                request.VideoId,
                request.PreviewId,
                request.Tags,
                request.Issues);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpPatch("{lessonId:guid}/position")]
        [Permission(Permissions.Lessons.UPDATE_LESSON)]
        public async Task<IActionResult> UpdateLessonPosition(
            [FromRoute] Guid lessonId,
            [FromBody] UpdateLessonPositionRequest request,
            [FromServices] UpdateLessonPositionHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new UpdateLessonPositionCommand(
                lessonId,
                request.ModuleId,
                request.Position);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpPatch("{lessonId}/tag")]
        [Permission(Permissions.Lessons.UPDATE_LESSON)]
        public async Task<IActionResult> AddTagToLesson(
            [FromRoute] Guid lessonId,
            [FromBody] Guid tagId,
            [FromServices] AddTagToLessonHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new AddTagToLessonCommand(lessonId, tagId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpPatch("{lessonId}/issue")]
        [Permission(Permissions.Lessons.UPDATE_LESSON)]
        public async Task<IActionResult> AddIssueToLesson(
            [FromRoute] Guid lessonId,
            [FromBody] Guid issueId,
            [FromServices] AddIssueToLessonHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new AddIssueToLessonCommand(lessonId, issueId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpPatch("{lessonId}/restore")]
        [Permission(Permissions.Lessons.UPDATE_LESSON)]
        public async Task<IActionResult> RestoreLesson(
            [FromRoute] Guid lessonId,
            [FromServices] RestoreLessonHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new RestoreLessonCommand(lessonId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpDelete("{lessonId}/issue")]
        [Permission(Permissions.Lessons.UPDATE_LESSON)]
        public async Task<IActionResult> RemoveIssueFromLesson(
            [FromRoute] Guid lessonId,
            [FromBody] Guid issueId,
            [FromServices] RemoveIssueFromLessonHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new RemoveIssueFromLessonCommand(lessonId, issueId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpDelete("{lessonId}/tag")]
        [Permission(Permissions.Lessons.UPDATE_LESSON)]
        public async Task<IActionResult> RemoveTagFromLesson(
            [FromRoute] Guid lessonId,
            [FromBody] Guid tagId,
            [FromServices] RemoveIssueFromLessonHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new RemoveIssueFromLessonCommand(lessonId, tagId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpDelete("{lessonId:guid}")]
        [Permission(Permissions.Lessons.DELETE_LESSON)]
        public async Task<IActionResult> SoftDeleteLesson(
            [FromRoute] Guid lessonId,
            [FromServices] SoftDeleteLessonHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new SoftDeleteLessonCommand(lessonId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }
    }
}
