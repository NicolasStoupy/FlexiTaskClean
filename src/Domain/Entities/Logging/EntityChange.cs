using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Logging
{
    public class EntityChange
    {
        public int EntityChangeId { get; set; }                // EntityChangeID (IDENTITY)
        public string Entity { get; set; } = null!;
        public string EntityField { get; set; } = null!;
        public string FieldType { get; set; } = null!;
        public string OldValue { get; set; } = null!;
        public string NewValue { get; set; } = null!;
        public DateTime? ChangedAt { get; set; }
        public string ChangedBy { get; set; } = null!;          // nvarchar(450) -> AspNetUsers.Id (mais pas de navigation Domain)
    }
}
