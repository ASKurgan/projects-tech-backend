using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.IssuesReviews;
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
    public class IssuesReviewRepository : IIssuesReviewRepository
    {
        private readonly IssuesDbContext _dbContext;

        public IssuesReviewRepository(IssuesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IssueReview, Error>> GetById(
            IssueReviewId id,
            CancellationToken cancellationToken = default)
        {
            var issueReview = await _dbContext.IssueReviews
                .Include(ir => ir.Comments)
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

            if (issueReview == null)
                return Errors.General.NotFound(id);

            return issueReview;
        }

        public async Task<Result<IssueReview, Error>> GetByUserIssueId(
            UserIssueId id,
            CancellationToken cancellationToken = default)
        {
            var issueReview = await _dbContext.IssueReviews
                .FirstOrDefaultAsync(i => i.UserIssueId == id, cancellationToken);

            if (issueReview == null)
                return Errors.General.NotFound(id);

            return issueReview;
        }

        public async Task<UnitResult<Error>> Add(IssueReview issueReview, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(issueReview, cancellationToken);

            return UnitResult.Success<Error>();
        }
    }
}
