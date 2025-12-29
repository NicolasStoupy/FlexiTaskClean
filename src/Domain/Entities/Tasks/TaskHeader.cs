using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks;

public class TaskHeader : BaseAuditableEntity
{
    public int TaskHeaderId { get; set; }
    public IList<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}
