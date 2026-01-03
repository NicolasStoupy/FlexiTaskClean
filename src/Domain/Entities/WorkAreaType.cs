using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class WorkAreaType : BaseAuditableEntity
    {
        public int WorkAreaTypeId { get; set; }                 // IDENTITY
        public string? Code { get; set; }                       // varchar(10) null
        public string? Label { get; set; }                      // varchar(50) null

        public IList<WorkArea>? WorkAreas { get; set; } = new List<WorkArea>();
    }
}
