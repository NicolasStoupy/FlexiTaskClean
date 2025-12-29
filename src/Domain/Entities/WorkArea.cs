using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class WorkArea
    {
        public int WorkAreaId { get; set; }

        public string Code { get; set; } = null!;

        public string CommonName { get; set; } = null!;

        public Plant Plant { get; set; } = null!;
    }
}
