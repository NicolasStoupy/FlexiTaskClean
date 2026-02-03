using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks.TaskSpecializations;

public class TransportTask : TaskItem
{
  
    public string? Support { get; set; } 
    public int? DestinationAreaId { get; set; }
    public int? SourceAreaId { get; set; } 

    public WorkArea DestinationArea { get; set; } = null!;
    public WorkArea SourceArea { get; set; } = null!;




}