using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.DynamicForms
{
    public class JsonSchemaTemplate : BaseAuditableEntity<int>
    {
        public string Label { get; set; } = null!;
        public string JsonSchema { get; set; } = null!;
        public int Version { get; set; }

        public List<TaskData> TaskDatas { get; set; } = new(); // optionnel
    }
}
