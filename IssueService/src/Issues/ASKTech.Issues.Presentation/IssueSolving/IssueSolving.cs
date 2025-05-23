﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using ASKTech.Framework;
using ASKTech.Framework.Authorization;
using ASKTech.Issues.Contracts.IssueSolving;
using ASKTech.Issues.Application.Features.IssueSolving.Queries.GetUserIssuesByModuleWithPagination;
using ASKTech.Issues.Application.Features.IssueSolving.Commands.TakeOnWork;
using ASKTech.Issues.Application.Features.IssueSolving.Commands.SendOnReview;
using ASKTech.Issues.Contracts.IssueReview;
using ASKTech.Issues.Application.Features.IssueSolving.Commands.StopWorking;

namespace ASKTech.Issues.Presentation.IssueSolving
{
    public class IssueSolvingController : ApplicationController
    {
        [HttpGet]
        public async Task<ActionResult> GetUserIssuesByModuleId(
            [FromQuery] GetUserIssuesByModuleWithPaginationRequest request,
            [FromServices] GetUserIssuesByModuleWithPaginationHandler handler,
            CancellationToken cancellationToken = default)
        {
            var query = new GetUserIssuesByModuleWithPaginationQuery(
                request.UserId,
                request.ModuleId,
                request.Status,
                request.Page,
                request.PageSize);

            var response = await handler.Handle(query, cancellationToken);

            return Ok(response);
        }

        [Permission(Permissions.SolvingIssues.CREATE_SOLVING_ISSUE)]
        [HttpPost("{issueId:guid}")]
        public async Task<ActionResult> TakeOnWork(
            [FromRoute] Guid moduleId,
            [FromRoute] Guid issueId,
            [FromServices] TakeOnWorkHandler handler,
            [FromServices] UserScopedData userScopedData,
            CancellationToken cancellationToken = default)
        {
            var command = new TakeOnWorkCommand(userScopedData.UserId, issueId, moduleId);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [Permission(Permissions.SolvingIssues.UPDATE_SOLVING_ISSUE)]
        [HttpPost("{userIssueId:guid}/review")]
        public async Task<ActionResult> SendOnReview(
            [FromRoute] Guid userIssueId,
            [FromServices] SendOnReviewHandler handler,
            [FromServices] UserScopedData userScopedData,
            [FromBody] SendOnReviewRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new SendOnReviewCommand(userIssueId, userScopedData.UserId, request.PullRequestUrl);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [Permission(Permissions.SolvingIssues.UPDATE_SOLVING_ISSUE)]
        [HttpPost("{userIssueId:guid}/cancel")]
        public async Task<ActionResult> StopWorking(
            [FromRoute] Guid userIssueId,
            [FromServices] StopWorkingHandler handler,
            [FromServices] UserScopedData userScopedData,
            CancellationToken cancellationToken = default)
        {
            var command = new StopWorkingCommand(userIssueId, userScopedData.UserId);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }
    }
}
