using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Traceability
{
    public class EntityChange : BaseEntity<int>
    {
        public string Entity { get; set; } = null!;
        public string EntityField { get; set; } = null!;
        public string FieldType { get; set; } = null!;
        public string OldValue { get; set; } = null!;
        public string NewValue { get; set; } = null!;
        public string EntityKey { get; set; } = null!;

        public DateTimeOffset ChangedAt { get; set; }

        // ✅ Domain ne connaît que l'ID utilisateur (pas ApplicationUser)
        public string ChangedByUserId { get; set; } = null!;
    }
}
