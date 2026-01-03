using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks
{
    public class TaskItemType
    {
        public string TaskItemTypeId { get; set; } = null!;     // TaskItemType_ID varchar(4) PK
        public string InstructionDescription { get; set; } = null!;
        public int JsonSchemaTemplateId { get; set; }

        public JsonSchemaTemplate? JsonSchemaTemplate { get; set; }

        public List<TaskItems> TaskItems { get; set; } = new();
    }
}
