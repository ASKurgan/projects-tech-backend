﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Framework;
using ASKTech.Framework.Authorization;
using ASKTech.Issues.Application.Features.Issue.Commands.AddIssue;
using ASKTech.Issues.Application.Features.Issue.Commands.DeleteIssue;
using ASKTech.Issues.Application.Features.Issue.Commands.DeleteIssue.ForceDeleteIssue;
using ASKTech.Issues.Application.Features.Issue.Commands.DeleteIssue.SoftDeleteIssue;
using ASKTech.Issues.Application.Features.Issue.Commands.RestoreIssue;
using ASKTech.Issues.Application.Features.Issue.Commands.UpdateIssueMainInfo;
using ASKTech.Issues.Application.Features.Issue.Queries.GetIssueById;
using ASKTech.Issues.Application.Features.Issue.Queries.GetIssuesByModuleWithPagination;
using ASKTech.Issues.Contracts.Issue;
using Microsoft.AspNetCore.Mvc;


namespace ASKTech.Issues.Presentation.Issues
{
    public class IssuesController : ApplicationController
    {
        [Permission(Permissions.Issues.READ_ISSUE)]
        [HttpGet("module/{moduleId:guid}")]
        public async Task<ActionResult> GetByModule(
            [FromRoute] Guid moduleId,
            [FromQuery] GetIssuesByModuleWithPaginationRequest request,
            [FromServices] GetIssuesByModuleWithPaginationHandler handler,
            CancellationToken cancellationToken)
        {
            var query = new GetFilteredIssuesByModuleWithPaginationQuery(
                moduleId,
                request.Title,
                request.SortBy,
                request.SortDirection,
                request.Page,
                request.PageSize);

            var response = await handler.Handle(query, cancellationToken);

            if (response.IsFailure)
                return response.Error.ToResponse();

            return Ok(response.Value);
        }

        [Permission(Permissions.Issues.READ_ISSUE)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(
            [FromRoute] Guid id,
            [FromServices] GetIssueByIdHandler handler,
            CancellationToken cancellationToken)
        {
            var query = new GetIssueByIdQuery(id);

            var response = await handler.Handle(query, cancellationToken);

            if (response.IsFailure)
                return response.Error.ToResponse();

            return Ok(response.Value);
        }

        [Permission(Permissions.Issues.CREATE_ISSUE)]
        [HttpPost]
        public async Task<ActionResult> AddIssue(
            [FromBody] AddIssueRequest request,
            [FromServices] CreateIssueHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new CreateIssueCommand(
                request.LessonId,
                request.ModuleId,
                request.Title,
                request.Description,
                request.Experience);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [Permission(Permissions.Issues.UPDATE_ISSUE)]
        [HttpPut("{issueId:guid}/main-info")]
        public async Task<ActionResult> UpdateIssueMainInfo(
            [FromRoute] Guid issueId,
            [FromBody] UpdateIssueMainInfoRequest request,
            [FromServices] UpdateIssueMainInfoHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new UpdateIssueMainInfoCommand(
                issueId,
                request.LessonId,
                request.ModuleId,
                request.Title,
                request.Description,
                request.Experience);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [Permission(Permissions.Issues.UPDATE_ISSUE)]
        [HttpPut("{issueId:guid}/restore")]
        public async Task<ActionResult> RestoreIssue(
            [FromRoute] Guid issueId,
            [FromServices] RestoreIssueHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new RestoreIssueCommand(issueId);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [Permission(Permissions.Issues.DELETE_ISSUE)]
        [HttpDelete("{issueId:guid}/soft")]
        public async Task<ActionResult> SoftDeleteIssue(
            [FromRoute] Guid issueId,
            [FromServices] SoftDeleteIssueHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new DeleteIssueCommand(issueId);
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [Permission(Permissions.Issues.DELETE_ISSUE)]
        [HttpDelete("{issueId:guid}/force")]
        public async Task<ActionResult> ForceDeleteIssue(
            [FromRoute] Guid issueId,
            [FromServices] ForceDeleteIssueHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new DeleteIssueCommand(issueId);
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
