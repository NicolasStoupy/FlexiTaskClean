using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks;

public class TaskStatus : BaseAuditableEntity
{
    public int TaskStatusId { get; set; }                   // IDENTITY
    public string Code { get; set; } = null!;               // varchar(5) unique
    public string Description { get; set; } = null!;        // varchar(50)

    public IList<TaskItems> TaskItems { get; set; } = new List<TaskItems>();
}