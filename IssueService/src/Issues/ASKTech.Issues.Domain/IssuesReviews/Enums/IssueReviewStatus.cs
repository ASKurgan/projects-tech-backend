using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.IssuesReviews.Enums
{
    public enum IssueReviewStatus
    {
        WaitingForReviewer,
        OnReview,
        Accepted,
        AskedForRevision
    }
}
