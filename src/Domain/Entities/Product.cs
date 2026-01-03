using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public int ProductId { get; set; }                      // int PK (pas identity)
        public string Description { get; set; } = null!;        // varchar(50)

        public List<Lot> Lots { get; set; } = new();
    }
}
