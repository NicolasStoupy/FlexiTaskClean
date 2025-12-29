using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks;

public  class TransportTask:TaskItem
{   

    public string? Support { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string? CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public int? DestinationArea { get; set; }

    public int? SourceArea { get; set; }

  

    public  TaskItem TaskItem { get; set; } = null!;
}
