using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Features.Plants.Commands.EventHandlers
{
    public class PlantDeletedEventHandler : INotificationHandler<PlantDeletedEvent>
    {

        private readonly ILogger<PlantDeletedEventHandler> _logger;

        public PlantDeletedEventHandler(ILogger<PlantDeletedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(PlantDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
