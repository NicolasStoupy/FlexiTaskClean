using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PlantIdentity:BaseAuditableEntity
    {
        public int PlantId { get; set; }
        public string Id_AspnetIdentity { get; set; } = null!;  // nvarchar(450)

        public Plant? Plant { get; set; }
    }
}
