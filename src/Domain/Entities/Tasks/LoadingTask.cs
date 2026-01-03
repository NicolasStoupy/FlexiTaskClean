using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks;

public partial class LoadingTask
{

    public int TaskHeaderId { get; set; }
    public int TaskItemsId { get; set; }

    public string Product { get; set; } = null!;
    public double Qty { get; set; }
    public string? Support { get; set; }

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }

    public int? AreaForLoading { get; set; }                // FK -> WorkAreaID (nullable)

    public TaskItems? TaskItem { get; set; }
    public WorkArea? LoadingArea { get; set; }
}
