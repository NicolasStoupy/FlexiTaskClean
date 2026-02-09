using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks
{
    public class TaskLog : BaseEntity
    {
        public int TaskLogID { get; private set; }

        public int TaskHeaderID { get; private set; }
        public int TaskItemID { get; private set; }

        public string EventType { get; private set; } = null!;  
        public string? OldStatus { get; private set; }
        public string? NewStatus { get; private set; }

        public DateTime OccurredAt { get; private set; }
        public string? PerformedBy { get; private set; }
        public string? Comment { get; private set; }

        protected TaskLog() { } // EF

        public TaskLog(int taskHeaderId, int taskItemId, string eventType, string? oldStatus, string? newStatus, string? performedBy, string? comment)
        {
            TaskHeaderID = taskHeaderId;
            TaskItemID = taskItemId;
            EventType = eventType;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            OccurredAt = DateTime.UtcNow;
            PerformedBy = performedBy;
            Comment = comment;
        }
    }

}
