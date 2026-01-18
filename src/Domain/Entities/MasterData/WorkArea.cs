using Domain.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.MasterData
{
    public class WorkArea : BaseAuditableEntity<int>
    {

        public string Code { get; set; } = null!;               // varchar(5) unique
        public string CommonName { get; set; } = null!;         // varchar(50) 
        public Plant Plant { get; set; } = null!;
        public WorkAreaType WorkAreaType { get; set; } = null!;
        public IList<Location> Locations { get; set; } = new List<Location>();
    }
}
