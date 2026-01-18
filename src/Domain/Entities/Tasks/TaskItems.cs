using Domain.Entities.DynamicForms;
using Domain.Entities.MasterData;
using Domain.Entities.Tasks.TaskSpecializations;

namespace Domain.Entities.Tasks;

public class TaskItem : BaseAuditableEntity
{
    // PK composite
    public int TaskHeaderId { get; set; }
    public int TaskItemId { get; set; }   // mappe TaskItemsID (IDENTITY)

    public bool StartingTask { get; set; }
    public bool EndingTask { get; set; }

    public int? LinkedWorkAreaId { get; set; }

    // Navigations
    public TaskHeader TaskHeader { get; set; } = null!;
    public TaskData TaskData { get; set; } = null!;
    public TaskItemType TaskItemType { get; set; } = null!;
    public TaskStatus TaskStatus { get; set; } = null!;
    public WorkArea? LinkedWorkArea { get; set; }
    public List<TaskItemDependency> Dependencies { get; set; } = new(); // "je dépends de"
    public List<TaskItemDependency> DependentBy { get; set; } = new();  // "dépend de moi"

    public TransportTask? TransportTask { get; set; } = null!;
    public LoadingTask? LoadingTask { get; set; }
}
