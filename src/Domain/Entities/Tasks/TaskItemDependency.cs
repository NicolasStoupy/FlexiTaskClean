using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks
{
    public class TaskItemDependency
    {
        // PK : (TaskHeaderID_DependOn, TaskItemsID_DependOn, TaskHeaderID, TaskItemsID)
        public int TaskHeaderIdDependOn { get; set; }
        public int TaskItemsIdDependOn { get; set; }

        public int TaskHeaderId { get; set; }
        public int TaskItemsId { get; set; }

        public TaskItems? DependOnTaskItem { get; set; }
        public TaskItems? TaskItem { get; set; }
    }
}
