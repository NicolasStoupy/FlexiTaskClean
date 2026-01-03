using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Lot
    {
        public string LocationId { get; set; } = null!;
        public string StorageId { get; set; } = null!;
        public string LotId { get; set; } = null!;              // varchar(10)

        public bool Blocked { get; set; }
        public int? PositionStorage { get; set; }

        public int ProductId { get; set; }

        public Storage? Storage { get; set; }
        public Product? Product { get; set; }
    }
}
