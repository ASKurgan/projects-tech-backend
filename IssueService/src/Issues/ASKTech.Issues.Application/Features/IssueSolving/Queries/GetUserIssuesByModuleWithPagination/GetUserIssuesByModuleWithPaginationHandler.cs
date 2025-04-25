using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Contracts.IssueSolving;
using ASKTech.Issues.Domain.IssueSolving.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Queries.GetUserIssuesByModuleWithPagination
{
    public class GetUserIssuesByModuleWithPaginationHandler
     : IQueryHandler<PagedList<UserIssueResponse>, GetUserIssuesByModuleWithPaginationQuery>
    {
        private readonly IIssuesReadDbContext _readDbContext;

        public GetUserIssuesByModuleWithPaginationHandler(IIssuesReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<PagedList<UserIssueResponse>> Handle(
            GetUserIssuesByModuleWithPaginationQuery query,
            CancellationToken cancellationToken)
        {
            var userIssuesQuery =
                from userIssue in _readDbContext.ReadUserIssues
                join issue in _readDbContext.ReadIssues
                    on userIssue.IssueId equals issue.Id
                where userIssue.UserId == query.UserId
                      && userIssue.ModuleId == query.ModuleId
                      && userIssue.Status == Enum.Parse<IssueStatus>(query.Status)
                orderby userIssue.Status
                select new UserIssueResponse
                {
                    Id = userIssue.Id,
                    UserId = userIssue.UserId,
                    IssueId = userIssue.IssueId,
                    ModuleId = userIssue.ModuleId,
                    IssueTitle = issue.Title.Value,
                    IssueDescription = issue.Description.Value,
                    Status = userIssue.Status.ToString(), //TODO: enum to russian string method
                    StartDateOfExecution = userIssue.StartDateOfExecution,
                    EndDateOfExecution = userIssue.EndDateOfExecution,
                    Attempts = userIssue.Attempts.Value,
                    PullRequestUrl = userIssue.PullRequestUrl.Value,
                };

            return await userIssuesQuery
                .ToPagedList(query.Page, query.PageSize, cancellationToken);
        }
    }
}
