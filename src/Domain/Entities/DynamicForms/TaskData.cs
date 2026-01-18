using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.DynamicForms
{
    public class TaskData : BaseAuditableEntity<int>
    {
        public string JsonData { get; set; } = null!;
        public string? ExternalLink { get; set; }

        public JsonSchemaTemplate JsonSchemaTemplate { get; set; } = null!;

        public List<TaskItem> TaskItems { get; set; } = new();
    }
}
