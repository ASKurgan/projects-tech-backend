using ASKTech.Issues.Domain.IssuesReviews;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Interfaces
{
    public interface IIssuesReviewRepository
    {
        Task<Result<IssueReview, Error>> GetById(
            IssueReviewId id,
            CancellationToken cancellationToken = default);

        Task<Result<IssueReview, Error>> GetByUserIssueId(
            UserIssueId id,
            CancellationToken cancellationToken = default);

        Task<UnitResult<Error>> Add(
            IssueReview issueReview,
            CancellationToken cancellationToken = default);
    }
}
