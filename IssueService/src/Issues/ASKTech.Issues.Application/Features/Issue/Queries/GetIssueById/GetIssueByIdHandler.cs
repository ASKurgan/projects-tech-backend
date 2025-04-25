using ASKTech.Core.Abstractions;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Contracts.Issue;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Issue.Queries.GetIssueById
{
    public class GetIssueByIdHandler : IQueryHandlerWithResult<IssueDto, GetIssueByIdQuery>
    {
        private readonly IIssuesReadDbContext _readDbContext;

        public GetIssueByIdHandler(IIssuesReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<IssueDto, ErrorList>> Handle(
            GetIssueByIdQuery query,
            CancellationToken cancellationToken = default)
        {
            var issueDto = await _readDbContext.ReadIssues
                .SingleOrDefaultAsync(i => i.Id == query.IssueId, cancellationToken);

            if (issueDto is null)
                return Errors.General.NotFound(query.IssueId).ToErrorList();

            var response = new IssueDto
            {
                Id = issueDto.Id,
                ModuleId = issueDto.ModuleId,
                Title = issueDto.Title.Value,
                Description = issueDto.Description.Value,
                LessonId = issueDto.LessonId?.Value,
            };

            return response;
        }
    }
}
