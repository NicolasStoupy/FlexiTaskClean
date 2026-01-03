using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Storage
    {
        public string LocationId { get; set; } = null!;         // varchar(10)
        public string StorageId { get; set; } = null!;          // varchar(10)

        public string Description { get; set; } = null!;
        public double LengthInMillimeters { get; set; }
        public bool Empty { get; set; }

        public Location? Location { get; set; }
        public List<Lot> Lots { get; set; } = new();
    }
}
