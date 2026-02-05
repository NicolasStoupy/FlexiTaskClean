using Domain.Events;

namespace Application.Features.TaskExecution
{
    public class TaskCompletedEventHandlers : INotificationHandler<TaskCompletedEvent>
    {
        public Task Handle(TaskCompletedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class TaskCreatedEventHandler : INotificationHandler<TaskCreatedEvent>
    {
        public Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class TaskInProgressEventHandler : INotificationHandler<TaskInProgressEvent>
    {
        public Task Handle(TaskInProgressEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class TaskStatusUpdatedEventHandler : INotificationHandler<TaskStatusUpdated>
    {
        public Task Handle(TaskStatusUpdated notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}