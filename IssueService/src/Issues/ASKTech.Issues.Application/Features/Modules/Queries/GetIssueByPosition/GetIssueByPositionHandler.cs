using ASKTech.Core.Abstractions;
using ASKTech.Issues.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.Queries.GetIssueByPosition
{
    public class GetIssueByPositionHandler : IQueryHandlerWithResult<Guid, GetIssueByPositionQuery>
    {
        private readonly IIssuesReadDbContext _readDbContext;

        public GetIssueByPositionHandler(IIssuesReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            GetIssueByPositionQuery query,
            CancellationToken cancellationToken = default)
        {
            var module = await _readDbContext.ReadModules
                .FirstOrDefaultAsync(i => i.Id == query.ModuleId, cancellationToken);

            if (module is null)
                return Errors.General.NotFound().ToErrorList();

            var issueDto = module.IssuesPosition
                .FirstOrDefault(i => i.Position == query.Position);

            if (issueDto is null)
                return Errors.General.NotFound().ToErrorList();

            return issueDto.IssueId.Value;
        }
    }
}
