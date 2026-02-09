using Domain.Common.Exceptions;
using Domain.Enums;
using Domain.Events;

namespace Domain.Entities.Tasks;

/// <summary>
/// Représente une unité de travail (tâche) dans un en-tête de workflow.
/// Contient l'état de la tâche, ses relations vers les tâches prérequises
/// et les tâches suivantes ainsi que des usines de création pour des types
/// de tâches (démarrage, fin, intermédiaire).
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
    /// Constructeur principal utilisé pour initialiser une instance de <see cref="TaskItem"/>.
    /// </summary>
    /// <param name="linkedArea">Identifiant de la zone de travail liée.</param>
    /// <param name="taskItemType">Type de la tâche (doit être non null et non vide).</param>
    /// <param name="target">Date cible optionnelle. Si null, la date courante UTC est utilisée.</param>
    /// <exception cref="ArgumentException">Si <paramref name="taskItemType"/> est null ou vide.</exception>
    public TaskItem(int linkedArea, string taskItemType, DateOnly? target)
    {
        if (string.IsNullOrWhiteSpace(taskItemType))
            throw new ArgumentException("taskItemType must be provided", nameof(taskItemType));

        LinkedWorkArea = linkedArea;
        TaskItemType = taskItemType;
        TargetDate = target ?? DateOnly.FromDateTime(DateTime.UtcNow);
    }

    /// <summary>
    /// Identifiant de l'en-tête de tâche (FK).
    /// </summary>
    public int TaskHeaderID { get; protected set; }

    /// <summary>
    /// Identifiant de la tâche (PK).
    /// </summary>
    public int TaskItemID { get; protected set; }

    /// <summary>
    /// Identifiant de la zone de travail liée.
    /// </summary>
    public int LinkedWorkArea { get; protected set; }

    /// <summary>
    /// Type métier de la tâche.
    /// </summary>
    public string TaskItemType { get; protected set; } = null!;

    /// <summary>
    /// Date cible de la tâche.
    /// </summary>
    public DateOnly TargetDate { get; protected set; }
    public string? TakenByUserId { get; protected set; }
    public DateTime? TakenAt { get; protected set; }
    public DateTime? LockExpiresAt { get; protected set; }


    private readonly List<TaskItemDependency> _prerequisites = new();

    /// <summary>
    /// Collection en lecture seule des dépendances qui sont des prérequis pour cette tâche.
    /// </summary>
    public IReadOnlyCollection<TaskItemDependency> Prerequisites => _prerequisites.AsReadOnly();

    private readonly List<TaskItemDependency> _nextSteps = new();

    /// <summary>
    /// Collection en lecture seule des dépendances vers les tâches suivantes (qui dépendent de cette tâche).
    /// </summary>
    public IReadOnlyCollection<TaskItemDependency> NextSteps => _nextSteps.AsReadOnly();

    /// <summary>
    /// État courant de la tâche.
    /// </summary>
    public TaskItemStatus TaskItemStatus { get; protected set; } = TaskItemStatus.NotStarted;

    /// <summary>
    /// Indique si la tâche est une tâche de démarrage dans le workflow.
    /// </summary>
    public bool StartingTask { get; protected set; }

    /// <summary>
    /// Indique si la tâche est une tâche de fin dans le workflow.
    /// </summary>
    public bool EndingTask { get; protected set; }

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
    /// Marque la tâche comme complétée puis met à jour les tâches suivantes qui peuvent devenir prêtes.
    /// Si la tâche est déjà complétée, l'appel est ignoré.
    /// </summary>
    public void Complete()
    {
        if (TaskItemStatus == TaskItemStatus.Completed)
            return;

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
    /// Indique si la tâche peut passer à l'état <see cref="TaskItemStatus.Ready"/>.
    /// Une tâche peut être prête si elle n'est ni <see cref="TaskItemStatus.Completed"/> ni <see cref="TaskItemStatus.InProgress"/>
    /// et que tous ses prérequis sont complétés.
    /// </summary>
    /// <returns>True si la tâche peut être mise en Ready; sinon False.</returns>
    public bool CanBeReady()
    {
        if (TaskItemStatus == TaskItemStatus.Completed ||
            TaskItemStatus == TaskItemStatus.InProgress)
            return false;

        return Prerequisites.All(f => f.DependsOn.TaskItemStatus == TaskItemStatus.Completed);
    }

    /// <summary>
    /// Indique si la tâche est actuellement dans l'état <see cref="TaskItemStatus.Ready"/>.
    /// </summary>
    /// <returns>True si Ready; sinon False.</returns>
    public bool HasReady() => TaskItemStatus == TaskItemStatus.Ready;

    /// <summary>
    /// Configure une tâche existante comme tâche de démarrage.
    /// Mettra la tâche en état Ready.
    /// </summary>
    /// <param name="taskItem">La tâche à configurer comme démarrage.</param>
    /// <returns>La même instance fournie en paramètre.</returns>
    /// <exception cref="ArgumentNullException">Si <paramref name="taskItem"/> est null.</exception>
    public  TaskItem SetStarting()
    {       

        StartingTask = true;
        EndingTask = false;
        TaskItemStatus = TaskItemStatus.Ready;

        return this;
    }

    

    /// <summary>
    /// Ajoute une dépendance "this -> nextTask" (c'est-à-dire que <paramref name="nextTask"/> dépend de cette tâche).
    /// Si la dépendance existe déjà, l'opération est ignorée.
    /// </summary>
    /// <param name="nextTask">Tâche suivante qui dépend de cette tâche.</param>
    /// <exception cref="ArgumentNullException">Si <paramref name="nextTask"/> est null.</exception>
    /// <exception cref="InvalidOperationException">Si on tente de créer une dépendance d'une tâche sur elle-même.</exception>
    public void AddNextStep(TaskItem nextTask)
    {
        if (nextTask is null) throw new ArgumentNullException(nameof(nextTask));
        if (ReferenceEquals(this, nextTask))
            throw new InvalidOperationException("A task cannot depend on itself.");

        // Si la relation existe déjà, on ne fait rien.
        if (this.NextSteps.Any(d => ReferenceEquals(d.TaskItem, nextTask)))
            return;

        var dependency = new TaskItemDependency
        {
            TaskItem = nextTask,
            DependsOn = this
        };

        // Si les IDs sont connus, les assigner pour cohérence avec EF après SaveChanges
        if (nextTask.TaskHeaderID > 0) dependency.TaskHeaderID = nextTask.TaskHeaderID;
        if (nextTask.TaskItemID > 0) dependency.TaskItemID = nextTask.TaskItemID;
        if (this.TaskHeaderID > 0) dependency.DependsOnTaskHeaderID = this.TaskHeaderID;
        if (this.TaskItemID > 0) dependency.DependsOnTaskItemID = this.TaskItemID;

        this._nextSteps.Add(dependency);
        nextTask._prerequisites.Add(dependency);
    }

    /// <summary>
    /// Ajoute une dépendance "prerequisite -> this" (c'est-à-dire que cette tâche dépend de <paramref name="prerequisite"/>).
    /// Symétrique à <see cref="AddNextStep(TaskItem)"/>.
    /// </summary>
    /// <param name="prerequisite">Tâche prérequis.</param>
    /// <exception cref="ArgumentNullException">Si <paramref name="prerequisite"/> est null.</exception>
    /// <exception cref="InvalidOperationException">Si on tente de créer une dépendance d'une tâche sur elle-même.</exception>
    public void AddPrerequisite(TaskItem prerequisite)
    {
        if (prerequisite is null) throw new ArgumentNullException(nameof(prerequisite));
        if (ReferenceEquals(this, prerequisite))
            throw new InvalidOperationException("A task cannot depend on itself.");

        if (this.Prerequisites.Any(d => ReferenceEquals(d.DependsOn, prerequisite)))
            return;

        var dependency = new TaskItemDependency
        {
            TaskItem = this,
            DependsOn = prerequisite
        };

        if (this.TaskHeaderID > 0) dependency.TaskHeaderID = this.TaskHeaderID;
        if (this.TaskItemID > 0) dependency.TaskItemID = this.TaskItemID;
        if (prerequisite.TaskHeaderID > 0) dependency.DependsOnTaskHeaderID = prerequisite.TaskHeaderID;
        if (prerequisite.TaskItemID > 0) dependency.DependsOnTaskItemID = prerequisite.TaskItemID;

        this._prerequisites.Add(dependency);
        prerequisite._nextSteps.Add(dependency);
    }

    /// <summary>
    /// Retire la relation "this -> nextTask" si elle est présente.
    /// </summary>
    /// <param name="nextTask">Tâche suivante à retirer.</param>
    /// <exception cref="ArgumentNullException">Si <paramref name="nextTask"/> est null.</exception>
    public void RemoveNextStep(TaskItem nextTask)
    {
        if (nextTask is null) throw new ArgumentNullException(nameof(nextTask));

        var dep = this._nextSteps.FirstOrDefault(d => ReferenceEquals(d.TaskItem, nextTask));
        if (dep is null) return;

        this._nextSteps.Remove(dep);
        nextTask._prerequisites.Remove(dep);
    }

    /// <summary>
    /// Retire la relation "prerequisite -> this" si elle est présente.
    /// </summary>
    /// <param name="prerequisite">Tâche prérequis à retirer.</param>
    /// <exception cref="ArgumentNullException">Si <paramref name="prerequisite"/> est null.</exception>
    public void RemovePrerequisite(TaskItem prerequisite)
    {
        if (prerequisite is null) throw new ArgumentNullException(nameof(prerequisite));

        var dep = this._prerequisites.FirstOrDefault(d => ReferenceEquals(d.DependsOn, prerequisite));
        if (dep is null) return;

        this._prerequisites.Remove(dep);
        prerequisite._nextSteps.Remove(dep);
    }

    /// <summary>
    /// Crée une tâche intermédiaire (ni démarrage ni fin) pour la zone indiquée.
    /// </summary>
    /// <param name="areaId">Identifiant de la zone de travail.</param>
    /// <returns>Nouvelle instance de <see cref="TaskItem"/>.</returns>
    public  TaskItem SetIntermediteTask()
    {

        StartingTask = false;
        EndingTask = false;
        TaskItemStatus = TaskItemStatus.NotStarted;
        return this;
        
    }
    public TaskItem SetEndingTask()
    {

        StartingTask = false;
        EndingTask = true;
        TaskItemStatus = TaskItemStatus.NotStarted;
        return this;

    }
    /// <summary>
    /// Effectue la transition d'état appropriée et publie les événements de domaine correspondants.
    /// - Si l'état est <see cref="TaskItemStatus.Ready"/>, passe à <see cref="TaskItemStatus.InProgress"/> et publie <see cref="TaskInProgressEvent"/>.
    /// - Si l'état est <see cref="TaskItemStatus.InProgress"/>, marque la tâche comme complétée et publie <see cref="TaskCompletedEvent"/>.
    /// Les autres états n'entraînent aucune action.
    /// </summary>
    public void Execute(string userID, TimeSpan lease)
    {
        switch (TaskItemStatus)
        {
            case TaskItemStatus.Ready:
                StartTask(userID, lease);
                break;

            case TaskItemStatus.InProgress:
                HandleInProgressTask(userID, lease);
                break;

            default:
                // Pour les autres états, on ne fait rien 
                break;
        }
    }

    /// <summary>
    /// Démarre la tâche : met l'état en <see cref="TaskItemStatus.InProgress"/>, applique un verrou et publie un événement.
    /// </summary>
    /// <param name="userId">Identifiant de l'utilisateur démarrant la tâche.</param>
    /// <param name="lease">Durée du verrou (lease) à appliquer.</param>
    private void StartTask(string userId, TimeSpan lease)
    {
        this.SetInProgress();
        ApplyLock(userId, lease);
        AddDomainEvent(new TaskInProgressEvent(this));
    }

    /// <summary>
    /// Gère l'appel lorsque la tâche est déjà en cours (<see cref="TaskItemStatus.InProgress"/>).
    /// - Si un autre utilisateur détient le verrou et que celui-ci n'est pas expiré, une exception est levée.
    /// - Sinon, la tâche est complétée pour l'utilisateur courant.
    /// </summary>
    /// <param name="userId">Identifiant de l'utilisateur tentant l'opération.</param>
    /// <param name="lease">Durée du verrou (lease) à appliquer en cas de reprise après expiration.</param>
    /// <exception cref="DomainException">Si un autre utilisateur détient encore le verrou et qu'il n'est pas expiré.</exception>
    private void HandleInProgressTask(string userId, TimeSpan lease)
    {
        // Si l'utilisateur est différent et que le verrou est toujours valide
        if (TakenByUserId != userId && !IsLockExpired())
        {
            throw new DomainException($"This task is currently locked by another user (expires at {LockExpiresAt}).");
        }

        // Soit c'est le même utilisateur, soit le verrou était expiré
        CompleteTask(userId, lease);
    }

    /// <summary>
    /// Finalise la tâche : met à jour le verrou (utile en cas de reprise après expiration), marque la tâche comme complétée
    /// et publie l'événement <see cref="TaskCompletedEvent"/>.
    /// </summary>
    /// <param name="userId">Identifiant de l'utilisateur complétant la tâche.</param>
    /// <param name="lease">Durée du verrou (lease) à appliquer.</param>
    private void CompleteTask(string userId, TimeSpan lease)
    {
        // Dans le cas d'une reprise après expiration, on met à jour les infos du preneur
        ApplyLock(userId, lease);

        this.Complete();
        AddDomainEvent(new TaskCompletedEvent(this));
    }

    /// <summary>
    /// Applique ou renouvelle le verrou de la tâche pour l'utilisateur spécifié pendant la durée indiquée.
    /// Met à jour : <see cref="TakenByUserId"/>, <see cref="TakenAt"/>, <see cref="LockExpiresAt"/>.
    /// </summary>
    /// <param name="userId">Identifiant de l'utilisateur qui prend le verrou.</param>
    /// <param name="lease">Durée du verrou (lease) à appliquer.</param>
    private void ApplyLock(string userId, TimeSpan lease)
    {
        TakenByUserId = userId;
        TakenAt = DateTime.UtcNow;
        LockExpiresAt = DateTime.UtcNow.Add(lease);
    }

    /// <summary>
    /// Indique si le verrou courant est expiré.
    /// </summary>
    /// <returns>True si la date d'expiration du verrou existe et est antérieure ou égale à l'instant UTC courant.</returns>
    private bool IsLockExpired()
    {
        return LockExpiresAt.HasValue && LockExpiresAt <= DateTime.UtcNow;
    }
}
