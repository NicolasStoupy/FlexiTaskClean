using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks.TaskSpecializations
{
    public class LoadingTaskLine
    {
        public int LineItemID { get; private set; } // PK

        public int TaskHeaderID { get; private set; }
        public int TaskItemID { get; private set; }

        public string Material { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public double Quantity { get; private set; }

        public LoadingTask LoadingTask { get; private set; } = null!;
    }
}
