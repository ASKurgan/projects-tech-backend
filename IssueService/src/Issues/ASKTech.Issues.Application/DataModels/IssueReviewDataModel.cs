﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.DataModels
{
    public record IssueReviewDataModel
    {
        public Guid Id { get; init; }

        public Guid UserIssueId { get; init; }

        public Guid UserId { get; init; }

        public Guid? ReviewerId { get; init; }

        public string IssueReviewStatus { get; init; } = string.Empty;

        public IEnumerable<CommentDataModel> Comments { get; init; } = [];

        public DateTime ReviewStartedTime { get; init; }

        public DateTime? IssueTakenTime { get; init; }

        public DateTime? IssueApprovedTime { get; init; }

        public string PullRequestLink { get; init; } = string.Empty;

        public Guid IssueReviewId { get; init; }
    }
}
