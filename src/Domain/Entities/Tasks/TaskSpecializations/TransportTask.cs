using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks.TaskSpecializations;

public class TransportTask : BaseAuditableEntity
{
    // PK + FK vers TaskItem
    public int TaskHeaderId { get; set; }
    public int TaskItemId { get; set; }

    public string? Support { get; set; }

    public int? DestinationAreaId { get; set; }
    public int? SourceAreaId { get; set; }

    public TaskItem TaskItem { get; set; } = null!;
    public WorkArea DestinationArea { get; set; } = null!;
    public WorkArea SourceArea { get; set; } = null!;

    
}