using Domain.Constants;

using Domain.Entities.MasterData;
using Domain.Entities.Tasks.TaskSpecializations;
using Domain.Enums;

namespace Domain.Entities.Tasks;

/// <summary>
/// Représente une unité de travail (tâche) dans un en-tête de workflow.
/// Contient son état, ses relations vers les tâches précédentes (prérequis)
/// et suivantes (next steps) ainsi que des usines de création pour des
/// types de tâches (démarrage, fin, intermédiaire).
/// Hérite de <see cref="BaseAuditableEntity"/> pour les informations d'audit.
/// </summary>
public class TaskItem : BaseAuditableEntity
{
    /// <summary>
    /// Constructeur par défaut requis par EF et la sérialisation.
    /// </summary>
    public TaskItem()
    {
    }
    public TaskItem(int linkedArea, string taskItemType)
    {
        LinkedWorkArea = linkedArea;
        TaskItemType = taskItemType;
    }
    public int TaskHeaderID { get; private set; }

    public int TaskItemID { get; private set; }

    public int LinkedWorkArea { get; private set; } 

    public string TaskItemType { get; private set; }

    public DateOnly TargetDate { get; set; }

    private readonly List<TaskItemDependency> _prerequisites =new(); // this depends on
    public IReadOnlyCollection<TaskItemDependency> Prerequisites => _prerequisites.AsReadOnly();


    private readonly List<TaskItemDependency> _nextSteps = new();    // depend on this
    public IReadOnlyCollection<TaskItemDependency> NextSteps => _nextSteps.AsReadOnly();


   
    
    
    public TaskItemStatus TaskItemStatus { get; private set; } = TaskItemStatus.NotStarted;

    public bool StartingTask { get; set; }

    public bool EndingTask { get; set; }

    public void SetNotStarted() => TaskItemStatus = TaskItemStatus.NotStarted;

    public void SetReady() => TaskItemStatus = TaskItemStatus.Ready;

    public void SetInProgress() => TaskItemStatus = TaskItemStatus.InProgress;

    public void SetCompleted() => TaskItemStatus = TaskItemStatus.Completed;

    public void Complete()
    {
        TaskItemStatus = TaskItemStatus.Completed;
        foreach (var next in NextSteps)
        {
            if (next.TaskItem.CanBeReady())
            {
                next.TaskItem.SetReady();
            }
        }
    }

    public bool CanBeReady()
    {
        if (TaskItemStatus == TaskItemStatus.Completed ||
            TaskItemStatus == TaskItemStatus.InProgress) return false;
        return Prerequisites.All(f => f.DependsOn.TaskItemStatus == TaskItemStatus.Completed);
    }

    public bool HasReady()
    {
        return TaskItemStatus == TaskItemStatus.Ready;
    }

    public static TaskItem CreateStarting(TaskItem taskItem)
    {
        taskItem.StartingTask = true;
        taskItem.EndingTask = false;
        taskItem.TaskItemStatus = TaskItemStatus.Ready;
      
        return taskItem;
     
    }

    public static TaskItem CreateEnding(int areaId)
    {
        return new TaskItem
        {
            LinkedWorkArea = areaId,
            StartingTask = false,
            EndingTask = true,
            TaskItemStatus = TaskItemStatus.NotStarted
        };
    }

    public void AddNextStep(TaskItem nextTask)
    {
        if (nextTask.TaskItemID == 0)
        {
            throw new ArgumentException("nextTask must have a valid TaskItemId (saved in DB).");
        }
        if (nextTask is null) throw new ArgumentNullException(nameof(nextTask));

        // Guard : déjà existant ?
        if (this.NextSteps.Any(d => d.TaskItem == nextTask))
            return;

        var dependency = new TaskItemDependency
        {
            // La tâche qui dépend = nextTask
            TaskItem = nextTask,
            // Le prérequis = this
            DependsOn = this,

            //  Set explicite des FK (après 1er SaveChanges elles sont connues)
            TaskHeaderID = nextTask.TaskHeaderID,
            TaskItemID = nextTask.TaskItemID,
            DependsOnTaskHeaderID = this.TaskHeaderID,
            DependsOnTaskItemID = this.TaskItemID
        };

        // Cohérence des navigations
        this._nextSteps.Add(dependency);          // "depend on this"
        nextTask._prerequisites.Add(dependency);  // "this depends on"
    }

    public static TaskItem CreateIntermediteTask(int areaId)
    {
        return new TaskItem
        {
            LinkedWorkArea = areaId,
            StartingTask = false,
            EndingTask = false,
            TaskItemStatus = TaskItemStatus.NotStarted
        };
    }

    public void Execute()
    {
        switch (TaskItemStatus)
        {
            case TaskItemStatus.Ready:
                this.SetInProgress();
                break;

            case TaskItemStatus.InProgress:
                this.Complete();
                break;

            case TaskItemStatus.Completed:

                break;

            case TaskItemStatus.Waiting:
                break;

            case TaskItemStatus.NotStarted:

                break;

            default:
                break;
        }
    }

    // PK composite
    //public int TaskHeaderId { get; set; }
    //public int TaskItemId { get; set; }   // mappe TaskItemsID (IDENTITY)

    //public bool StartingTask { get; set; }
    //public bool EndingTask { get; set; }
    //public int? LinkedWorkAreaId { get; set; }

    //// Navigations
    //public TaskHeader TaskHeader { get; set; } = null!;
    //public TaskData TaskData { get; set; } = null!;
    //public TaskItemType TaskItemType { get; set; } = null!;
    //public TaskStatus TaskStatus { get; set; } = null!;
    //public WorkArea? LinkedWorkArea { get; set; }
    //public List<TaskItemDependency> Dependencies { get; set; } = new(); // "je dépends de"
    //public List<TaskItemDependency> DependentBy { get; set; } = new();  // "dépend de moi"

    //public TransportTask? TransportTask { get; set; } = null!;
    //public LoadingTask? LoadingTask { get; set; }
}