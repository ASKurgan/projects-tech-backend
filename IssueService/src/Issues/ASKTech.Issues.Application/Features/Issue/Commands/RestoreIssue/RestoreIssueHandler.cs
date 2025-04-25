using ASKTech.Core.Abstractions;
using ASKTech.Core.Database;
using ASKTech.Issues.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Issue.Commands.RestoreIssue
{
    public class RestoreIssueHandler : ICommandHandler<Guid, RestoreIssueCommand>
    {
        private readonly IIssuesRepository _issuesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RestoreIssueHandler> _logger;

        public RestoreIssueHandler(
            IIssuesRepository issuesRepository,
            IUnitOfWork unitOfWork,
            ILogger<RestoreIssueHandler> logger)
        {
            _issuesRepository = issuesRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            RestoreIssueCommand command,
            CancellationToken cancellationToken = default)
        {
            var restoreResult = await _issuesRepository
                .GetById(command.IssueId, true, cancellationToken);

            if (restoreResult.IsFailure)
                return restoreResult.Error.ToErrorList();

            restoreResult.Value.Restore();

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation(
                "Issue {issueId} was restored",
                command.IssueId);

            return command.IssueId;
        }
    }
}
