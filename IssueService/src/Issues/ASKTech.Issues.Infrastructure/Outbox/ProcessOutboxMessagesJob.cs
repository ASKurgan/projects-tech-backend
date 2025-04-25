using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Infrastructure.Outbox
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private readonly ProcessOutboxMessagesService _processOutboxMessagesService;

        public ProcessOutboxMessagesJob(ProcessOutboxMessagesService processOutboxMessagesService)
        {
            _processOutboxMessagesService = processOutboxMessagesService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _processOutboxMessagesService.Execute(context.CancellationToken);
        }
    }
}
