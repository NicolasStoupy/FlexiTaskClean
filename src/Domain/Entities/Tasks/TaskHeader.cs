using Domain.Common.Exceptions;

namespace Domain.Entities.Tasks;

public class TaskHeader : BaseAuditableEntity
{
    public int TaskHeaderID { get; private set; }
    private readonly List<TaskItem> _taskItems = new();
    public IReadOnlyCollection<TaskItem> TaskItems => _taskItems.AsReadOnly();

    public TaskItem AddStartingTask(TaskItem taskItem)
    {       
        
        // une seule starting task
        if (TaskItems.Any(t => t.StartingTask))
            throw new DomainException("A starting task already exists for this workflow.");

        TaskItem.CreateStarting(taskItem);

        _taskItems.Add(taskItem);

        return taskItem;
    }

    public static TaskHeader Create()
    {
        return new TaskHeader();
    }

    public TaskItem AddEndingTask(int areaId)
    {
        if (areaId <= 0)
            throw new DomainException("AreaId must be provided to create a ending task.");

        // une seule ending task
        if (TaskItems.Any(t => t.EndingTask))
            throw new DomainException("A ending task already exists for this workflow.");

        var taskItem = TaskItem.CreateEnding(areaId);

        _taskItems.Add(taskItem);

        return taskItem;
    }

    public TaskItem AddIntermediateTask(int areaId)
    {
        if (areaId <= 0)
            throw new DomainException("AreaId must be provided to create a intermediate task.");

        var taskItem = TaskItem.CreateIntermediteTask(areaId);

        _taskItems.Add(taskItem);

        return taskItem;
    }

    public List<TaskItem>? GetNextsRunnableTasks()
    {
        var runnableTasks = new List<TaskItem>();
        foreach (var taskItem in TaskItems)
        {
            if (taskItem.HasReady())
            {
                runnableTasks.Add(taskItem);
            }
        }
        return runnableTasks;
    }
}