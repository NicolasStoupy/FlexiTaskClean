using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.MasterData
{
    public class WorkAreaTransportSupport
    {
        public int WorkAreaID { get; set; }          // colonne réelle dans WA_TransportSupport
        public string SupportTypeID { get; set; } = "";
    }

}
