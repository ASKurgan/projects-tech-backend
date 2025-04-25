using ASKTech.Issues.Domain.Issue;
using ASKTech.Issues.Domain.IssueSolving.Entities;
using ASKTech.Issues.Domain.IssuesReviews;
using ASKTech.Issues.Domain.Lesson;
using ASKTech.Issues.Domain.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Interfaces
{
    public interface IIssuesReadDbContext
    {
        IQueryable<Module> ReadModules { get; }

        IQueryable<Issue> ReadIssues { get; }

        IQueryable<UserIssue> ReadUserIssues { get; }

        IQueryable<IssueReview> ReadIssueReviews { get; }

        IQueryable<Lesson> ReadLessons { get; }
    }
}
