using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.MasterData
{
    public class PlantIdentity:BaseAuditableEntity<int>
    {
       
        public string Id_AspnetIdentity { get; set; } = null!;  // nvarchar(450)

        public Plant? Plant { get; set; }
    }
}
