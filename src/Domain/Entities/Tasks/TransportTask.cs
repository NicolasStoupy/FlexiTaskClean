using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks;

public class TransportTask
{
    public int TaskHeaderId { get; set; }
    public int TaskItemsId { get; set; }

    public string? Support { get; set; }

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public string? CreatedTime { get; set; }                // oui varchar(50) dans SQL
    public DateTime? UpdatedTime { get; set; }

    public int? DestinationArea { get; set; }               // FK -> WorkAreaID (nullable)
    public int? SourceArea { get; set; }                    // FK -> WorkAreaID (nullable)

    public TaskItems? TaskItem { get; set; }
    public WorkArea? DestinationWorkArea { get; set; }
    public WorkArea? SourceWorkArea { get; set; }
}
