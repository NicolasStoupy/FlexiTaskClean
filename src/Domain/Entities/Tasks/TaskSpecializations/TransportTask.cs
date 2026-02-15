using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks.TaskSpecializations;

/// <summary>
/// Représente une tâche de transport entre deux zones de travail.
/// </summary>
public class TransportTask : TaskItem
{
    /// <summary>
    /// Support ou moyen utilisé pour le transport (optionnel).
    /// </summary>
    public string? Support { get; private set; }

    /// <summary>
    /// Identifiant de la zone de destination.
    /// </summary>
    public int DestinationAreaId { get; private set; }

    /// <summary>
    /// Identifiant de la zone source.
    /// </summary>
    public int SourceAreaId { get; private set; }

    /// <summary>
    /// Zone de destination (peuplant par l'ORM).
    /// </summary>
    public WorkArea DestinationArea { get; private set; } = null!;

    /// <summary>
    /// Zone source (peuplant par l'ORM).
    /// </summary>
    public WorkArea SourceArea { get; private set; } = null!;

    /// <summary>
    /// Crée une nouvelle instance de <see cref="TransportTask"/>.
    /// </summary>
    /// <param name="support">Support ou moyen de transport.</param>
    /// <param name="destinationAreaId">Id de la zone de destination.</param>
    /// <param name="sourceAreaId">Id de la zone source.</param>
    /// <param name="assignedAreaId">Id de la zone assignée (transmis au type de base).</param>
    /// <param name="targetDate">Date cible (optionnelle).</param>
    internal TransportTask(
        string? support,
        int destinationAreaId,
        int sourceAreaId,
        int assignedAreaId,
        DateOnly? targetDate)
        : base(assignedAreaId, "Transport", targetDate)
    {
        Support = support;
        DestinationAreaId = destinationAreaId;
        SourceAreaId = sourceAreaId;
    }

    /// <summary>
    /// Constructeur sans paramètre requis par certains ORM et frameworks.
    /// </summary>
    public TransportTask()
    {
    }

    public override string ToHumanString()
    {
        if (SourceArea.Code == DestinationArea.Code)
            return $"=> {DestinationArea.Code}";
        return $"{SourceArea.Code} => {DestinationArea.Code}";
    }

   
      
    
}