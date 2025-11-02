using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plant.EventHandlers
{
    public class PlantDeletedEventHandler : INotificationHandler<Domain.Events.PlantDeletedEvent>
    {

        private readonly ILogger _logger;
        public PlantDeletedEventHandler(ILogger<PlantCreatedEventHandler> logger)
        {
            _logger = logger;

        }
        public Task Handle(PlantDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("FlexiTask Domain Event: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;

        }
    }
}
