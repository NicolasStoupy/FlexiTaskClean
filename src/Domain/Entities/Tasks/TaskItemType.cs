using Domain.Entities.DynamicForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks
{
    public class TaskItemType : BaseAuditableEntity<string>
    {
        public string InstructionDescription { get; set; } = null!;

        public int JsonSchemaTemplateId { get; set; }
        public JsonSchemaTemplate JsonSchemaTemplate { get; set; } = null!;

        public List<TaskItem> TaskItems { get; set; } = new(); // optionnel
    }
}
