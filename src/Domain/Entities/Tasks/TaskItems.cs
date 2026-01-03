namespace Domain.Entities.Tasks;

public  class TaskItems
{
    // PK composite : (TaskHeaderID, TaskItemsID)
    public int TaskHeaderId { get; set; }
    public int TaskItemsId { get; set; }                    // identity(1,1) mais PK composite
    public TaskItems? TaskItem { get; set; }
    public bool StartingTask { get; set; }
    public bool EndingTask { get; set; }

    public int TaskDataId { get; set; }
    public string TaskItemTypeId { get; set; } = null!;
    public int TaskStatusId { get; set; }

    public int? LinkedWorkArea { get; set; }                // FK nullable -> WorkArea.WorkAreaID

    // Navigations
    public TaskHeader? TaskHeader { get; set; }
    public TaskData? TaskData { get; set; }
    public TaskItemType? TaskItemType { get; set; }
    public TaskStatus? TaskStatus { get; set; }
    public WorkArea? WorkArea { get; set; }

    // One-to-one
    public LoadingTask? LoadingTask { get; set; }
    public TransportTask? TransportTask { get; set; }

    // Dependencies
    public List<TaskItemDependency> Dependencies { get; set; } = new();      // this -> depends on others
    public List<TaskItemDependency> DependedBy { get; set; } = new();        // others -> depend on this
}