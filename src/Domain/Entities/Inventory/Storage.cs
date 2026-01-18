using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Inventory
{
    public class Storage : BaseAuditableEntity<string>
    {
        public string LocationId { get; set; } = null!;
        public string StorageId { get; set; } = null!;

        public string Description { get; set; } = null!;
        public double LengthInMillimeters { get; set; }
        public bool Empty { get; set; }

        public Location Location { get; set; } = null!;
        public List<Lot> Lots { get; set; } = new();
    }
}
