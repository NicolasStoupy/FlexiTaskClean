using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.MasterData
{
    public class WorkAreaTransport :WorkArea
    {
        public WorkAreaTransport(int plantID,string truckName , double maxLoad) 
        
        {
            MaxLoad = maxLoad;
            TruckName = truckName;


        }
        public string TruckName { get; private set; } = "";
        public double MaxLoad { get; private set; }


        private readonly List<SupportType> _supportedTypes = new();
        public IReadOnlyCollection<SupportType> SupportedTypes => _supportedTypes.AsReadOnly();

        public void LinkSupportType(SupportType supportType)
        {
            if (supportType is null)
                throw new ArgumentNullException(nameof(supportType));

            if (_supportedTypes.Any(x => x.SupportTypeID == supportType.SupportTypeID))
                return;

            _supportedTypes.Add(supportType);
        }

        public void UnlinkSupportType(string supportTypeID)
        {
            // idempotent : si pas trouvé -> ne fait rien
            var toRemove = _supportedTypes.FirstOrDefault(x => x.SupportTypeID == supportTypeID);
            if (toRemove is null)
                return;

            _supportedTypes.Remove(toRemove);
        }



    }
}
