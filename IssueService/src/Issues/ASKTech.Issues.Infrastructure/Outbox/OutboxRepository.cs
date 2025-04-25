using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASKTech.Issues.Infrastructure.Outbox
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly IssuesDbContext _dbContext;

        public OutboxRepository(IssuesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add<T>(T message, CancellationToken cancellationToken)
        {
            var outboxMessages = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.Now,
                Type = typeof(T).FullName!,
                Payload = JsonSerializer.Serialize(message),
            };

            await _dbContext.AddAsync(outboxMessages, cancellationToken);
        }
    }
}
