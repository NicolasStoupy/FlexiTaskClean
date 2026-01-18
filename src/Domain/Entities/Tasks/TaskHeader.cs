using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks;

public class TaskHeader : BaseAuditableEntity<int>
{
 
    
    public List<TaskItem> Items { get; set; } = new();
}
