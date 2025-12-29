using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

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

    public class PlantActivatedEventHandler : INotificationHandler<PlantActivatedEvent>
    {
        private readonly ILogger<PlantActivatedEventHandler> _logger;

        public PlantActivatedEventHandler(ILogger<PlantActivatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(PlantActivatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Plant Activated {notification.Plant.Code} le {notification.Plant.LastModified} par {notification.Plant.LastModifiedBy}");

            return Task.CompletedTask;
        }
    }
}