using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks.TaskSpecializations;

public class TransportTask : TaskItem
{
    public string? Support { get; private set; }
    public int DestinationAreaId { get; private set; }
    public int SourceAreaId { get; private set; }

    public WorkArea DestinationArea { get; private set; } = null!;
    public WorkArea SourceArea { get; private set; } = null!;

    public TransportTask(string? support, int destinationAreaId, int sourceAreaId) : base(sourceAreaId, "Transport")
    {
        Support = support;
        DestinationAreaId = destinationAreaId;
        SourceAreaId = sourceAreaId;

    }
}