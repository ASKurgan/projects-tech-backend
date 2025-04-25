using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.Issue;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using ASKTech.Issues.Infrastructure.DbContexts;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Infrastructure.Repositories
{
    public class IssuesesRepository : IIssuesRepository
    {
        private readonly IssuesDbContext _dbContext;

        public IssuesesRepository(IssuesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Issue issue, CancellationToken cancellationToken = default)
        {
            await _dbContext.Issues.AddAsync(issue, cancellationToken);
            return issue.Id;
        }

        public Guid Save(Issue issue, CancellationToken cancellationToken = default)
        {
            _dbContext.Issues.Attach(issue);
            return issue.Id.Value;
        }

        public Guid Delete(Issue issue)
        {
            _dbContext.Issues.Remove(issue);

            return issue.Id;
        }

        public async Task<Result<Issue, Error>> GetById(
            IssueId issueId,
            bool includeDeletedOption = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Issue> query = _dbContext.Issues;

            if (includeDeletedOption)
                query = query.IgnoreQueryFilters();

            var issue = await query
                .FirstOrDefaultAsync(m => m.Id == issueId, cancellationToken);

            if (issue is null)
                return Errors.General.NotFound(issueId);

            return issue;
        }

        public async Task<Result<Issue, Error>> GetByIdForRestore(
            IssueId issueId, CancellationToken cancellationToken = default)
        {
            var issue = await _dbContext.Issues
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(m => m.Id == issueId, cancellationToken);

            if (issue is null)
                return Errors.General.NotFound(issueId);

            return issue;
        }

        public async Task<Result<Issue, Error>> GetByTitle(
            Title title, CancellationToken cancellationToken = default)
        {
            var issue = await _dbContext.Issues
                .FirstOrDefaultAsync(m => m.Title == title, cancellationToken);

            if (issue is null)
                return Errors.General.NotFound();

            return issue;
        }
    }
}
