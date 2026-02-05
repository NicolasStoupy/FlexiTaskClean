using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.MasterData
{
    public class WorkAreaType : BaseAuditableEntity
    {
        public int WorkAreaTypeID { get;private set; }

        public string? Code { get; set; }                       // varchar(10) null
        public string? Label { get; set; }                      // varchar(50) null

       
    }
}
