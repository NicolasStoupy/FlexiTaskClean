using Domain.Common.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Traceability
{
    public class EntityChange : BaseEntity
    {
        private EntityChange() { } // EF Core

        public EntityChange(EntityChangeParams p)
        {
            if (p is null) throw new ArgumentNullException(nameof(p));
            if (string.IsNullOrWhiteSpace(p.Entity)) throw new DomainException("Entity is required");
            if (string.IsNullOrWhiteSpace(p.EntityField)) throw new DomainException("EntityField is required");
            if (string.IsNullOrWhiteSpace(p.FieldType)) throw new DomainException("FieldType is required");
            if (string.IsNullOrWhiteSpace(p.EntityKey)) throw new DomainException("EntityKey is required");
            if (string.IsNullOrWhiteSpace(p.ChangedByUserId)) throw new DomainException("ChangedByUserId is required");

            Entity = p.Entity.Trim();
            EntityField = p.EntityField.Trim();
            FieldType = p.FieldType.Trim();

            // Old/New peuvent être null (ex: création/suppression), on stocke "" si tu veux éviter null en DB
            OldValue = p.OldValue ?? string.Empty;
            NewValue = p.NewValue ?? string.Empty;

            EntityKey = p.EntityKey.Trim();
            ChangedByUserId = p.ChangedByUserId.Trim();

            ChangedAt = DateTimeOffset.UtcNow;
        }

        public int EntityChangeID { get; private set; }

        public string Entity { get; private set; } = null!;
        public string EntityField { get; private set; } = null!;
        public string FieldType { get; private set; } = null!;
        public string OldValue { get; private set; } = null!;
        public string NewValue { get; private set; } = null!;
        public string EntityKey { get; private set; } = null!;

        public DateTimeOffset ChangedAt { get; private set; }

        public string ChangedByUserId { get; private set; } = null!;
    }


}
