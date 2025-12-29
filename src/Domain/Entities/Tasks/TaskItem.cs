namespace Domain.Entities.Tasks;

public  class TaskItem
{
    public int TaskHeaderId { get; set; }

    public int TaskItemsId { get; set; }

    public bool StartingTask { get; set; }

    public bool EndingTask { get; set; }

    public int TaskDataId { get; set; }

    public string TaskItemTypeId { get; set; } = null!;

    public int TaskStatusId { get; set; }

    public int? LinkedWorkArea { get; set; }

    public TaskHeader TaskHeader { get; set; } = null!;  

    public TaskStatus TaskStatus { get; set; } = null!;
}