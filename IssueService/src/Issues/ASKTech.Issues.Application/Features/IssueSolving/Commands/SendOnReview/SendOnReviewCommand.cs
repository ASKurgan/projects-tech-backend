﻿using ASKTech.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.Commands.SendOnReview
{
    public record SendOnReviewCommand(Guid UserIssueId, Guid UserId, string PullRequestUrl) : ICommand;
}
