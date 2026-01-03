using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks
{
    public class JsonSchemaTemplate : BaseAuditableEntity
    {
        public int JsonSchemaTemplateId { get; set; }           // IDENTITY
        public string Label { get; set; } = null!;
        public string JsonSchema { get; set; } = null!;
        public int Version { get; set; }
    }
}
