using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Inventory
{
    public class Lot: BaseAuditableEntity
    {
        // PK composite
        public string LocationId { get; set; } = null!;
        public string StorageId { get; set; } = null!;
        public string LotId { get; set; } = null!;

        public bool Blocked { get; set; }
        public int? PositionStorage { get; set; }

        // FK Product
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        // Navigation vers Storage (FK composite)
        public Storage Storage { get; set; } = null!;
    }
}
