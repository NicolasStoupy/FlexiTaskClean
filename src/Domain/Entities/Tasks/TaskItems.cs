using Domain.Constants;
using Domain.Entities.DynamicForms;
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

    /// <summary>
    /// Identifiant de l'en-tête de tâches auquel appartient cet item.
    /// Fait partie de la clé composite (avec <see cref="TaskItemId"/>).
    /// </summary>
    public int TaskHeaderId { get; set; }

    /// <summary>
    /// Identifiant de l'item au sein de l'en-tête de tâches.
    /// Fait partie de la clé composite.
    /// </summary>
    public int TaskItemId { get; set; }

    /// <summary>
    /// Identifiant de la zone de travail liée à cette tâche.
    /// </summary>
    public int LinkedWorkArea { get; set; }

    /// <summary>
    /// Liste des dépendances où cet item dépend d'autres tasks (prérequis).
    /// Navigation technique (table de jointure).
    /// </summary>
    public List<TaskItemDependency> Prerequisites { get; set; } = new(); // this depends on

    /// <summary>
    /// Liste des dépendances vers les tâches qui dépendent de cet item.
    /// Navigation technique (table de jointure).
    /// </summary>
    public List<TaskItemDependency> NextSteps { get; set; } = new();    // depend on this

    /// <summary>
    /// État courant de la tâche. Le setter est privé : utiliser les méthodes
    /// de modification d'état exposées (SetReady, SetInProgress, Complete, ...).
    /// Valeur par défaut : <see cref="TaskItemStatus.NotStarted"/>.
    /// </summary>
    public TaskItemStatus TaskItemStatus { get; private set; } = TaskItemStatus.NotStarted;

    /// <summary>
    /// Indique si cette tâche est une tâche de démarrage du workflow.
    /// </summary>
    public bool StartingTask { get; set; }

    /// <summary>
    /// Indique si cette tâche est une tâche de fin du workflow.
    /// </summary>
    public bool EndingTask { get; set; }

    /// <summary>
    /// Définit l'état à <see cref="TaskItemStatus.NotStarted"/>.
    /// </summary>
    public void SetNotStarted() => TaskItemStatus = TaskItemStatus.NotStarted;

    /// <summary>
    /// Définit l'état à <see cref="TaskItemStatus.Ready"/>.
    /// </summary>
    public void SetReady() => TaskItemStatus = TaskItemStatus.Ready;

    /// <summary>
    /// Définit l'état à <see cref="TaskItemStatus.InProgress"/>.
    /// </summary>
    public void SetInProgress() => TaskItemStatus = TaskItemStatus.InProgress;

    /// <summary>
    /// Définit l'état à <see cref="TaskItemStatus.Completed"/>.
    /// </summary>
    public void SetCompleted() => TaskItemStatus = TaskItemStatus.Completed;

    /// <summary>
    /// Marque cette tâche comme complétée puis tente de basculer les tâches
    /// suivantes (<see cref="NextSteps"/>) en état <see cref="TaskItemStatus.Ready"/>
    /// si leurs prérequis sont remplis.
    /// </summary>
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

    /// <summary>
    /// Indique si cette tâche peut passer en état "Ready".
    /// Conditions :
    /// - Elle n'est ni déjà complétée ni en cours.
    /// - Tous ses prérequis ont le statut <see cref="TaskItemStatus.Completed"/>.
    /// </summary>
    /// <returns>True si la tâche peut être prête, false sinon.</returns>
    public bool CanBeReady()
    {
        if (TaskItemStatus == TaskItemStatus.Completed ||
            TaskItemStatus == TaskItemStatus.InProgress) return false;
        return Prerequisites.All(f => f.DependsOn.TaskItemStatus == TaskItemStatus.Completed);
    }

    /// <summary>
    /// Indique si la tâche est actuellement en état <see cref="TaskItemStatus.Ready"/>.
    /// </summary>
    public bool HasReady()
    {
        return TaskItemStatus == TaskItemStatus.Ready;
    }

    /// <summary>
    /// Usine : crée une tâche de démarrage liée à la zone indiquée.
    /// La tâche de démarrage est directement en état <see cref="TaskItemStatus.Ready"/>.
    /// </summary>
    /// <param name="areaId">Identifiant de la zone de travail liée.</param>
    /// <returns>Nouvelle instance de <see cref="TaskItem"/> marquée comme starting.</returns>
    public static TaskItem CreateStarting(int areaId)
    {
        return new TaskItem
        {
            LinkedWorkArea = areaId,
            StartingTask = true,
            EndingTask = false,
            TaskItemStatus = TaskItemStatus.Ready
        };
    }

    /// <summary>
    /// Usine : crée une tâche de fin liée à la zone indiquée.
    /// La tâche de fin démarre par défaut en <see cref="TaskItemStatus.NotStarted"/>.
    /// </summary>
    /// <param name="areaId">Identifiant de la zone de travail liée.</param>
    /// <returns>Nouvelle instance de <see cref="TaskItem"/> marquée comme ending.</returns>
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

    /// <summary>
    /// Ajoute une dépendance vers la tâche suivante (<paramref name="nextTask"/>).
    /// - Vérifie que <paramref name="nextTask"/> n'est pas null.
    /// - Évite les doublons.
    /// - Construit la relation <see cref="TaskItemDependency"/> en renseignant
    ///   les FK explicites et en synchronisant les collections de navigation.
    /// </summary>
    /// <param name="nextTask">La tâche qui dépendra de cette tâche.</param>
    /// <exception cref="ArgumentNullException">Si <paramref name="nextTask"/> est null.</exception>
    public void AddNextStep(TaskItem nextTask)
    {
        if (nextTask.TaskItemId == 0)
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
            TaskHeaderId = nextTask.TaskHeaderId,
            TaskItemId = nextTask.TaskItemId,
            DependsOnTaskHeaderId = this.TaskHeaderId,
            DependsOnTaskItemId = this.TaskItemId
        };

        // Cohérence des navigations
        this.NextSteps.Add(dependency);          // "depend on this"
        nextTask.Prerequisites.Add(dependency);  // "this depends on"
    }

    /// <summary>
    /// Usine : crée une tâche intermédiaire liée à la zone indiquée.
    /// Par défaut elle est ni starting ni ending et à l'état NotStarted.
    /// </summary>
    /// <param name="areaId">Identifiant de la zone de travail liée.</param>
    /// <returns>Nouvelle instance de <see cref="TaskItem"/> intermédiaire.</returns>
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
