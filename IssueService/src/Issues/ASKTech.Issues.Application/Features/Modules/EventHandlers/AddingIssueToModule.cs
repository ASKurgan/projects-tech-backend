using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.Issue.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Modules.EventHandlers
{
    public class AddingIssueToModule : INotificationHandler<IssueCreatedEvent>
    {
        private readonly IModulesRepository _modulesRepository;

        public AddingIssueToModule(IModulesRepository modulesRepository)
        {
            _modulesRepository = modulesRepository;
        }

        public async Task Handle(IssueCreatedEvent notification, CancellationToken cancellationToken)
        {
            var moduleResult = await _modulesRepository.GetById(notification.ModuleId, cancellationToken);

            moduleResult.Value.AddIssue(notification.IssueId);
        }
    }
}
