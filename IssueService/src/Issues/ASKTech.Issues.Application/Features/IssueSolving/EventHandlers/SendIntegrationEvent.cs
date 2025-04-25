using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.IssueSolving.DomainEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.IssueSolving.EventHandlers
{
    public class SendIntegrationEvent : INotificationHandler<IssueSentOnReviewEvent>
    {
        private readonly IOutboxRepository _outboxRepository;

        public SendIntegrationEvent(IOutboxRepository outboxRepository)
        {
            _outboxRepository = outboxRepository;
        }

        public async Task Handle(IssueSentOnReviewEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new Contracts.Messaging.IssueSentOnReviewEvent(
                domainEvent.UserId,
                domainEvent.UserIssueId.Value,
                Guid.Empty);

            await _outboxRepository.Add(
                integrationEvent,
                cancellationToken);
        }
    }
}
