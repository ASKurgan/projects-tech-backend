using ASKTech.Issues.Domain.IssueSolving.Entities;
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
    public interface IUserIssueRepository
    {
        Task<Guid> Add(UserIssue userIssue, CancellationToken cancellationToken = default);

        Task<Result<UserIssue, Error>> GetUserIssueById(
            UserIssueId userIssueId,
            CancellationToken cancellationToken = default);
    }
}
