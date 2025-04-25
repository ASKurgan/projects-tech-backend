using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.IssueReview
{
    public class CommentResponse
    {
        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public Guid IssueReviewId { get; init; }

        public required string Message { get; init; }

        public DateTime CreatedAt { get; init; }
    }
}
