using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Core.Validation;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.IssuesReviews.Entities;
using ASKTech.Issues.Domain.IssuesReviews.ValueObjects;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssuesReviews.Commands.AddComment
{
    public class AddCommentHandler : ICommandHandler<Guid, AddCommentCommand>
    {
        private readonly IIssuesReviewRepository _issuesReviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddCommentCommand> _validator;
        private readonly ILogger<AddCommentHandler> _logger;

        public AddCommentHandler(
            IIssuesReviewRepository issuesReviewRepository,
            IUnitOfWork unitOfWork,
            IValidator<AddCommentCommand> validator,
            ILogger<AddCommentHandler> logger)
        {
            _issuesReviewRepository = issuesReviewRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            AddCommentCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToList();

            var issueReviewResult = await _issuesReviewRepository
                .GetById(IssueReviewId.Create(command.IssueReviewId), cancellationToken);

            if (issueReviewResult.IsFailure)
                return issueReviewResult.Error.ToErrorList();

            var message = Message.Create(command.Message).Value;

            var comment = Comment.Create(UserId.Create(command.UserId), message);

            // Хоть и проверка всегда вернет false на будущее если валидация внутри Comment будет присутствовать
            // оставим это поле.
            if (comment.IsFailure)
                return comment.Error.ToErrorList();

            var addCommentResult = issueReviewResult.Value.AddComment(comment.Value);

            if (addCommentResult.IsFailure)
                return addCommentResult.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation(
                "Comment {commentId} was created in issueReview {issueReviewId}",
                comment.Value.Id.Value,
                command.IssueReviewId);

            return comment.Value.Id.Value;
        }
    }
}
