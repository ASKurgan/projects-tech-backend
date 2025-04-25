using ASKTech.Issues.Domain.IssuesReviews.ValueObjects;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.IssuesReviews.Entities
{
    public class Comment : Entity<CommentId>
    {
        // Ef core
        private Comment(CommentId id)
            : base(id)
        {
        }

        private Comment(
            CommentId id,
            UserId userId,
            Message message,
            DateTime createdAt)
            : base(id)
        {
            UserId = userId;
            Message = message;
            CreatedAt = createdAt;
        }

        public IssueReview? IssueReview { get; private set; }

        public UserId UserId { get; private set; } = null!;

        public Message Message { get; private set; } = null!;

        public DateTime CreatedAt { get; private set; }

        public static Result<Comment, Error> Create(
            UserId userId,
            Message message)
        {
            return Result.Success<Comment, Error>(new Comment(
                CommentId.NewCommentId(),
                userId,
                message,
                DateTime.UtcNow));
        }
    }
}
