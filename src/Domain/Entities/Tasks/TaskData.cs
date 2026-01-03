using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks
{
    public class TaskData : BaseAuditableEntity
    {
        public int TaskDataId { get; set; }                     // IDENTITY
        public string JsonData { get; set; } = null!;           // varchar(max)
        public int JsonSchemaTemplateId { get; set; }

        public JsonSchemaTemplate? JsonSchemaTemplate { get; set; }

        public List<TaskItems> TaskItems { get; set; } = new();
    }
}
