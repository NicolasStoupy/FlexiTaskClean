using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks;

public class TaskStatus : BaseAuditableEntity<int>
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<TaskItem> TaskItems { get; set; } = new(); // optionnel
}