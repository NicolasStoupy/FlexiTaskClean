using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class WorkArea : BaseAuditableEntity
    {
        public int WorkAreaId { get; set; }                     // IDENTITY
        public string Code { get; set; } = null!;               // varchar(5) unique
        public string CommonName { get; set; } = null!;         // varchar(50)

        //public int PlantId { get; set; }
        
        //public int WorkAreaTypeId { get; set; }
        public Plant Plant { get; set; } = null!;
        public WorkAreaType WorkAreaType { get; set; } = null!;
        public IList<Location> Locations { get; set; } = new List<Location>();
    }
}
