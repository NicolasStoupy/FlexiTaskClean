using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks.TaskSpecializations;

public class LoadingTask : BaseAuditableEntity
{
    public int TaskHeaderId { get; set; }
    public int TaskItemId { get; set; }

    public string Product { get; set; } = null!;
    public double Qty { get; set; }
    public string? Support { get; set; }

    public int? AreaForLoadingId { get; set; }

    public TaskItem TaskItem { get; set; } = null!;
    public WorkArea? AreaForLoading { get; set; }
}
