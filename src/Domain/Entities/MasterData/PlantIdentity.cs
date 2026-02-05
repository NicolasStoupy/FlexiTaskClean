using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.MasterData
{
    public class PlantIdentity:BaseAuditableEntity
    {
        public int PlantID { get; private set; }
        public string AspNetIdentityID { get; private set; } = null!;  // nvarchar(450)

    
    }
}
