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
    public class PlantCreatedEventHandler : INotificationHandler<PlantCreatedEvent>
    {
        private readonly ILogger<PlantCreatedEventHandler> _logger;

        public PlantCreatedEventHandler(ILogger<PlantCreatedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(PlantCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("FlexiTask Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
