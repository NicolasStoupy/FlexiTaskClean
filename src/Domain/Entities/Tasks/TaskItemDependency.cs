using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks
{
    public class TaskItemDependency : BaseAuditableEntity
    {
        // PK composite (4 colonnes)
        public int TaskHeaderId { get; set; }
        public int TaskItemId { get; set; }

        public int DependsOnTaskHeaderId { get; set; }
        public int DependsOnTaskItemId { get; set; }

        // Navigations
        public TaskItem TaskItem { get; set; } = null!;
        public TaskItem DependsOn { get; set; } = null!;
    }
}
