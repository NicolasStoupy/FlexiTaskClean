using Domain.Common.Exceptions;

namespace Domain.Entities.Tasks;

/// <summary>
/// Représente l'en-tête d'un workflow de tâches contenant la collection des éléments de tâche.
/// </summary>
/// <remarks>
/// Fournit des méthodes de création et d'ajout pour des tâches de type départ, intermédiaire et fin,
/// ainsi qu'une méthode utilitaire permettant d'obtenir les tâches prêtes à être exécutées.
/// </remarks>
public class TaskHeader : BaseAuditableEntity<int>
{
    /// <summary>
    /// Liste des éléments de tâche (noeuds) appartenant à ce workflow.
    /// </summary>
    public List<TaskItem> TaskItems { get; set; } = new();

    /// <summary>
    /// Ajoute et retourne une tâche de départ pour ce workflow.
    /// </summary>
    /// <param name="areaId">Identifiant de la zone (doit être > 0).</param>
    /// <returns>Le <see cref="TaskItem"/> créé en tant que tâche de départ.</returns>
    /// <exception cref="DomainException">
    /// Levée si <paramref name="areaId"/> n'est pas fourni (<= 0) ou si une tâche de départ existe déjà.
    /// </exception>
    public TaskItem AddStartingTask(int areaId,TaskItem taskItem)
    {
        if (areaId <= 0)
            throw new DomainException("AreaId must be provided to create a starting task.");

        // une seule starting task
        if (TaskItems.Any(t => t.StartingTask))
            throw new DomainException("A starting task already exists for this workflow.");

         TaskItem.CreateStarting(areaId,taskItem);

        TaskItems.Add(taskItem);

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
    /// Ajoute et retourne une tâche de fin pour ce workflow.
    /// </summary>
    /// <param name="areaId">Identifiant de la zone (doit être > 0).</param>
    /// <returns>Le <see cref="TaskItem"/> créé en tant que tâche de fin.</returns>
    /// <exception cref="DomainException">
    /// Levée si <paramref name="areaId"/> n'est pas fourni (<= 0) ou si une tâche de fin existe déjà.
    /// </exception>
    public TaskItem AddEndingTask(int areaId)
    {
        if (areaId <= 0)
            throw new DomainException("AreaId must be provided to create a ending task.");

        // une seule ending task
        if (TaskItems.Any(t => t.EndingTask))
            throw new DomainException("A ending task already exists for this workflow.");

        var taskItem = TaskItem.CreateEnding(areaId);

        TaskItems.Add(taskItem);

        return taskItem;
    }

    /// <summary>
    /// Ajoute et retourne une tâche intermédiaire pour ce workflow.
    /// </summary>
    /// <param name="areaId">Identifiant de la zone (doit être > 0).</param>
    /// <returns>Le <see cref="TaskItem"/> créé en tant que tâche intermédiaire.</returns>
    /// <exception cref="DomainException">Levée si <paramref name="areaId"/> n'est pas fourni (<= 0).</exception>
    public TaskItem AddIntermediateTask(int areaId)
    {
        if (areaId <= 0)
            throw new DomainException("AreaId must be provided to create a intermediate task.");

        var taskItem = TaskItem.CreateIntermediteTask(areaId);

        TaskItems.Add(taskItem);

        return taskItem;
    }

    /// <summary>
    /// Obtient la liste des tâches suivantes prêtes à être exécutées.
    /// </summary>
    /// <returns>
    /// Une liste des <see cref="TaskItem"/> dont l'état interne indique qu'elles sont prêtes (<c>HasReady()</c>).
    /// La méthode retourne une liste vide si aucune tâche n'est prête.
    /// </returns>
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