using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Inventory
{
    public class Location : BaseAuditableEntity<string>
    {
        public string Label { get; set; } = null!;
        public int WorkAreaId { get; set; }

        public WorkArea WorkArea { get; set; } = null!;

    }
}
