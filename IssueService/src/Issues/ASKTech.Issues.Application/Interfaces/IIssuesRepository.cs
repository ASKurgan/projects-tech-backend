using ASKTech.Issues.Domain.Issue;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace ASKTech.Issues.Application.Interfaces
{
    public interface IIssuesRepository
    {
        Task<Guid> Add(Issue issue, CancellationToken cancellationToken = default);

        Guid Save(Issue issue, CancellationToken cancellationToken = default);

        Guid Delete(Issue issue);

        Task<Result<Issue, Error>> GetById(
            IssueId issueId,
            bool includeDeletedOption = false,
            CancellationToken cancellationToken = default);

        Task<Result<Issue, Error>> GetByTitle(Title title, CancellationToken cancellationToken = default);
    }
}
