using Domain.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Domain.Entities.MasterData
{
    public class WorkArea : BaseAuditableEntity
    {

        public WorkArea(int plantID, int workAreaTypeID, string code, string commonName,  bool active)
        {          
            PlantID = plantID;
            WorkAreaTypeID = workAreaTypeID;
            Code = code;
            CommonName = commonName;
            Active = active;
        }
        public int WorkAreaID { get; private set; }
        public int PlantID { get; private set; }
        public int WorkAreaTypeID { get; private set; }


        public string Code { get; private set; } = null!;               // varchar(5) unique
        public string CommonName { get; private set; } = null!;         // varchar(50) 
        public bool Active { get; private set; } = true;

        private readonly List<Location> _locations = new();
        public IReadOnlyCollection<Location> Locations => _locations.AsReadOnly();
        public WorkAreaType WorkAreaType { get; private set; } = null!;
        public void Update(string code, string commonName, int plantId, bool active)
        {
            Code = code;
            CommonName = commonName;
            PlantID = plantId;
            Active = active;
        }
    }
}
