using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Location
    {
        public string LocationId { get; set; } = null!;         // varchar(10) PK
        public string Label { get; set; } = null!;              // varchar(20) unique
        public int WorkAreaId { get; set; }

        public WorkArea? WorkArea { get; set; }
        public List<Storage> Storages { get; set; } = new();
    }
}
