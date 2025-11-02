using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class PlantCreatedEvent : BaseEvent
    {

        public Plant Plant { get; }
        public PlantCreatedEvent(Plant plant)
        {
            Plant = plant;
        }
    }
}
