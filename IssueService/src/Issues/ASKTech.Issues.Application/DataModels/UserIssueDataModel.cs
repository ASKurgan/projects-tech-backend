﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.DataModels
{
    public class UserIssueDataModel
    {
        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public Guid IssueId { get; init; }

        public Guid ModuleId { get; init; }

        public string Status { get; init; } = string.Empty;

        public DateTime StartDateOfExecution { get; init; }

        public DateTime EndDateOfExecution { get; init; }

        public int Attempts { get; init; }

        public string PullRequestUrl { get; init; } = string.Empty;
    }
}
