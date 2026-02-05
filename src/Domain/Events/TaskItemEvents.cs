using Domain.Entities.Tasks;
namespace Domain.Events;

public record TaskCompletedEvent : BaseEvent
{
    public TaskItem Task { get; }

    public TaskCompletedEvent(TaskItem taskItem)
    {
        Task = taskItem ?? throw new ArgumentNullException(nameof(taskItem));
    }
}
public record TaskCreatedEvent : BaseEvent
{
    public TaskItem Task { get; }

    public TaskCreatedEvent(TaskItem taskItem)
    {
        Task = taskItem ?? throw new ArgumentNullException(nameof(taskItem));
    }
}
public record TaskInProgressEvent : BaseEvent
{
    public TaskItem Task { get; }

    public TaskInProgressEvent(TaskItem taskItem)
    {
        Task = taskItem ?? throw new ArgumentNullException(nameof(taskItem));
    }
}
public record TaskStatusUpdated : BaseEvent
{
    public TaskItem Task { get; }

    public TaskStatusUpdated(TaskItem taskItem)
    {
        Task = taskItem ?? throw new ArgumentNullException(nameof(taskItem));
    }
}