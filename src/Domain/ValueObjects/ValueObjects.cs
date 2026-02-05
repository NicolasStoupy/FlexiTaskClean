using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public record EntityChangeParams(
     string Entity,
     string EntityField,
     string FieldType,
     string OldValue,
     string NewValue,
     string EntityKey,
     string ChangedByUserId);
}
