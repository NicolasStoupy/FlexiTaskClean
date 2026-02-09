using Application.Common.Interfaces;
using Domain.Entities.Tasks;
using Domain.Events;

namespace Application.Features.TaskExecution
{
    public class TaskCompletedEventHandlers(IApplicationDbContextFactory factory,IUser user) : INotificationHandler<TaskCompletedEvent>
    {
        private readonly IApplicationDbContextFactory _factory= factory;
        private readonly IUser _user = user;
        public async Task Handle(TaskCompletedEvent notification, CancellationToken cancellationToken)
        {
            await using var context = await _factory.CreateAsync(cancellationToken);

            var performedBy =
                !string.IsNullOrWhiteSpace(_user.Id) ? _user.Id :
                !string.IsNullOrWhiteSpace(_user.Id) ? _user.Id :
                null;

            var log = new TaskLog(
                taskHeaderId: notification.Task.TaskHeaderID,
                taskItemId: notification.Task.TaskItemID,
                eventType: "Completed",
                oldStatus: TaskItemStatus.InProgress.ToString(),
                newStatus: TaskItemStatus.Completed.ToString(),
                performedBy: performedBy,
                comment: null
            );

            context.TaskLogs.Add(log);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public class TaskCreatedEventHandler(IApplicationDbContextFactory factory,IUser user) : INotificationHandler<TaskCreatedEvent>
    {
        private readonly IApplicationDbContextFactory _factory = factory;
        private readonly IUser _user = user;

        public async Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
        {
            await using var context = await _factory.CreateAsync(cancellationToken);

            var performedBy =
                !string.IsNullOrWhiteSpace(_user.Id) ? _user.Id :
                !string.IsNullOrWhiteSpace(_user.Id) ? _user.Id :
                null;

            var log = new TaskLog(
                taskHeaderId: notification.Task.TaskHeaderID,
                taskItemId: notification.Task.TaskItemID,
                eventType: "Created",
                oldStatus: "",
                newStatus: TaskItemStatus.NotStarted.ToString(),
                performedBy: performedBy,
                comment: null
            );

            context.TaskLogs.Add(log);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public class TaskInProgressEventHandler(IApplicationDbContextFactory factory, IUser user) : INotificationHandler<TaskInProgressEvent>
    {
        private readonly IApplicationDbContextFactory _factory = factory;
        private readonly IUser _user = user;
        public async Task Handle(TaskInProgressEvent notification, CancellationToken cancellationToken)
        {
            await using var context = await _factory.CreateAsync(cancellationToken);

            var performedBy =
                !string.IsNullOrWhiteSpace(_user.Id) ? _user.Id :
                !string.IsNullOrWhiteSpace(_user.Id) ? _user.Id :
                null;

            var log = new TaskLog(
                taskHeaderId: notification.Task.TaskHeaderID,
                taskItemId: notification.Task.TaskItemID,
                eventType: "Inprogress",
                oldStatus: TaskItemStatus.Ready.ToString(),
                newStatus: TaskItemStatus.InProgress.ToString(),
                performedBy: performedBy,
                comment: null
            );

            context.TaskLogs.Add(log);
            await context.SaveChangesAsync(cancellationToken);
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