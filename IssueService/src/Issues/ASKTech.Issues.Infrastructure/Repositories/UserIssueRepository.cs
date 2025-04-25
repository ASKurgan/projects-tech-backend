using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.IssueSolving.Entities;
using ASKTech.Issues.Domain.ValueObjects.Ids;
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
    public class UserIssueRepository : IUserIssueRepository
    {
        private readonly IssuesDbContext _dbContext;

        public UserIssueRepository(IssuesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(UserIssue userIssue, CancellationToken cancellationToken = default)
        {
            await _dbContext.UserIssues.AddAsync(userIssue, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return userIssue.Id;
        }

        public async Task<Result<UserIssue, Error>> GetUserIssueById(
            UserIssueId userIssueId,
            CancellationToken cancellationToken = default)
        {
            var userIssue =
                await _dbContext.UserIssues.SingleOrDefaultAsync(ui => ui.Id == userIssueId, cancellationToken);

            if (userIssue is null)
                return Errors.General.NotFound();

            return userIssue;
        }
    }
}
