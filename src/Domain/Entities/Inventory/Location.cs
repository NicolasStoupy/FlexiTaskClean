using Domain.Common.Exceptions;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Inventory
{
    public class Location : BaseEntity
    {
        private Location() { } // EF

        public Location(string locationId, string label, int workAreaId)
        {
            if (string.IsNullOrWhiteSpace(locationId)) throw new DomainException("LocationID required");
            if (string.IsNullOrWhiteSpace(label)) throw new DomainException("Label required");
            if (workAreaId <= 0) throw new DomainException("WorkAreaId invalid");

            LocationID = locationId.Trim();
            Label = label.Trim();
            WorkAreaId = workAreaId;
        }

        public string LocationID { get; private set; } = null!;
        public string Label { get; private set; } = null!;
        public int WorkAreaId { get; private set; }
        public WorkArea WorkArea { get; private set; } = null!;
    }
}
