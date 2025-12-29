using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public record PlantDeletedEvent : BaseEvent
    {
        public PlantDeletedEvent(Plant plant)
        {
            Plant = plant;
        }

        public Plant Plant { get; }
    }
}
