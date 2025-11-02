using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class PlantCompletedEvent:BaseEvent
    {
        public Plant Plant { get; }
        public PlantCompletedEvent(Plant plant)
        {
            Plant = plant;
        }
    }
}
