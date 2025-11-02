

namespace Domain.Events
{
    public class PlantDeletedEvent: BaseEvent
    {
        public Plant Plant { get; }
        public PlantDeletedEvent(Plant plant)
        {
            Plant = plant;
        }
    }
}
