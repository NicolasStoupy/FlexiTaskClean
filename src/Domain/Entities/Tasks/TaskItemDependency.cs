using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks
{
    public class TaskItemDependency : BaseAuditableEntity
    {
        // PK composite (4 colonnes)
        public int TaskHeaderID { get; set; }
        public int TaskItemID { get; set; }

        public int DependsOnTaskHeaderID { get; set; }
        public int DependsOnTaskItemID { get; set; }
        public TaskItem TaskItem { get; set; } = null!;
        public TaskItem DependsOn { get; set; } = null!;

    }
}
