using Domain.Common.Exceptions;

namespace Domain.Entities.Tasks;

/// <summary>
/// Représente l'en-tête d'un workflow de tâches.
/// Contient la collection immuable des <see cref="TaskItem"/> associées
/// et fournit des méthodes pour ajouter des tâches de différents types
/// (starting, intermediate, ending) ainsi qu'une méthode pour récupérer
/// les tâches prêtes à s'exécuter.
/// </summary>
public class TaskHeader : BaseAuditableEntity
{
    /// <summary>
    /// Identifiant du TaskHeader.
    /// </summary>
    public int TaskHeaderID { get; private set; }

    private readonly List<TaskItem> _taskItems = new();

    /// <summary>
    /// Collection en lecture seule des <see cref="TaskItem"/> associées à ce workflow.
    /// </summary>
    public IReadOnlyCollection<TaskItem> TaskItems => _taskItems.AsReadOnly();

    /// <summary>
    /// Ajoute une tâche de type "starting" au workflow.
    /// </summary>
    /// <param name="taskItem">L'instance de <see cref="TaskItem"/> à ajouter comme starting task.</param>
    /// <returns>La même instance <paramref name="taskItem"/> ajoutée.</returns>
    /// <exception cref="DomainException">
    /// Levée si une starting task existe déjà pour ce workflow.
    /// </exception>
    public TaskItem AddStartingTask(TaskItem taskItem)
    {
        // Une seule starting task autorisée
        if (TaskItems.Any(t => t.StartingTask))
            throw new DomainException("A starting task already exists for this workflow.");
        taskItem.SetStarting();

        _taskItems.Add(taskItem);

        return taskItem;
    }

    /// <summary>
    /// Crée une nouvelle instance de <see cref="TaskHeader"/>.
    /// </summary>
    /// <returns>Nouvelle instance de <see cref="TaskHeader"/>.</returns>
    public static TaskHeader Create()
    {
        return new TaskHeader();
    }

    /// <summary>
    /// Ajoute une tâche de type "ending" au workflow pour l'aire spécifiée.
    /// </summary>
    /// <param name="areaId">Identifiant de l'aire associée à la ending task (doit être > 0).</param>
    /// <returns>La <see cref="TaskItem"/> créée et ajoutée en tant que ending task.</returns>
    /// <exception cref="DomainException">
    /// Levée si <paramref name="areaId"/> est invalide ou si une ending task existe déjà.
    /// </exception>
    public TaskItem AddEndingTask(TaskItem taskItem)
    {
        // Une seule ending task autorisée
        if (TaskItems.Any(t => t.EndingTask))
            throw new DomainException("A ending task already exists for this workflow.");
        taskItem.SetEndingTask();

        _taskItems.Add(taskItem);

        return taskItem;
    }

    /// <summary>
    /// Ajoute une tâche intermédiaire au workflow pour l'aire spécifiée.
    /// </summary>
    /// <param name="areaId">Identifiant de l'aire associée à la tâche intermédiaire (doit être > 0).</param>
    /// <returns>La <see cref="TaskItem"/> créée et ajoutée en tant que tâche intermédiaire.</returns>
    /// <exception cref="DomainException">Levée si <paramref name="areaId"/> est invalide.</exception>
    public TaskItem AddIntermediateTask(TaskItem taskItem)
    {
        taskItem.SetIntermediteTask();

        _taskItems.Add(taskItem);

        return taskItem;
    }

    /// <summary>
    /// Récupère la liste des tâches suivantes pouvant être exécutées (état "ready").
    /// </summary>
    /// <returns>Liste non nulle de <see cref="TaskItem"/> prêtes à s'exécuter. Peut être vide.</returns>
    public List<TaskItem> GetNextsRunnableTasks()
    {
        // Utilise LINQ pour filtrer et retourner la liste des tâches prêtes.
        return TaskItems.Where(t => t.HasReady()).ToList();
    }
}