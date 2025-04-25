using ASKTech.Issues.Domain.IssueSolving.DomainEvents;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Issues.Domain.IssueSolving.Enums;
using ASKTech.Issues.Domain.IssueSolving.ValueObjects;

namespace ASKTech.Issues.Domain.IssueSolving.Entities
{
    public class UserIssue : DomainEntity<UserIssueId>
    {
        public UserIssue(
            UserIssueId id,
            Guid userId,
            IssueId issueId,
            ModuleId moduleId)
            : base(id)
        {
            UserId = userId;
            IssueId = issueId;
            ModuleId = moduleId;

            TakeOnWork();
        }

        // ef core
        private UserIssue(UserIssueId id)
            : base(id)
        {
        }

        public Guid UserId { get; private set; }

        public IssueId IssueId { get; private set; } = null!;

        public ModuleId ModuleId { get; private set; } = null!;

        public IssueStatus Status { get; private set; }

        public DateTime StartDateOfExecution { get; private set; }

        public DateTime EndDateOfExecution { get; private set; }

        public Attempts Attempts { get; private set; } = null!;

        public PullRequestUrl PullRequestUrl { get; private set; } = PullRequestUrl.Empty;

        public UnitResult<Error> SendOnReview(PullRequestUrl pullRequestUrl)
        {
            if (Status != IssueStatus.AtWork)
                return Error.Failure("issue.status.invalid", "issue not at work");

            Status = IssueStatus.UnderReview;
            PullRequestUrl = pullRequestUrl;

            var domainEvent = new IssueSentOnReviewEvent(Id, UserId, pullRequestUrl);
            AddDomainEvent(domainEvent);

            return Result.Success<Error>();
        }

        public UnitResult<Error> SendForRevision()
        {
            if (Status != IssueStatus.UnderReview)
                return Error.Failure("issue.status.invalid", "issue status should be not completed or under review");

            Status = IssueStatus.AtWork;
            Attempts = Attempts.Add();

            return Result.Success<Error>();
        }

        public UnitResult<Error> StopWorking()
        {
            if (Status != IssueStatus.AtWork)
                return Error.Failure("issue.status.invalid", "issue status should be at work");

            Status = IssueStatus.NotAtWork;

            return Result.Success<Error>();
        }

        public UnitResult<Error> CompleteIssue()
        {
            if (Status != IssueStatus.UnderReview)
                return Error.Failure("issue.invalid.status", "issue status should be under review");

            EndDateOfExecution = DateTime.UtcNow;
            Status = IssueStatus.Completed;

            return new UnitResult<Error>();
        }

        private void TakeOnWork()
        {
            StartDateOfExecution = DateTime.UtcNow;
            Status = IssueStatus.AtWork;
            Attempts = Attempts.Create();
        }
    }
}
