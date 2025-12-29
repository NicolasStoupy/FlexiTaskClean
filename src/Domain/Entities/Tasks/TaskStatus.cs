using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks;

public  class TaskStatus
{
    public int TaskStatusId { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public  IList<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}
