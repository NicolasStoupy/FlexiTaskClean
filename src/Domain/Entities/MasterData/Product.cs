using Domain.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.MasterData
{
    public class Product : BaseAuditableEntity<int>
    {
        
        public string Description { get; set; } = null!;

        public List<Lot> Lots { get; set; } = new();
    }
}
