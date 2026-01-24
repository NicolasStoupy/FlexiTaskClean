using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    /// <summary>
    /// Événement immuable signalant la suppression d'une instance de <see cref="Plant"/>.
    /// Hérite de <see cref="BaseEvent"/>.
    /// </summary>
    public record PlantDeletedEvent : BaseEvent
    {
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="PlantDeletedEvent"/>.
        /// </summary>
        /// <param name="plant">L'entité <see cref="Plant"/> qui a été supprimée.</param>
        public PlantDeletedEvent(Plant plant)
        {
            Plant = plant;
        }

        /// <summary>
        /// Obtient la plante supprimée associée à cet événement.
        /// </summary>
        public Plant Plant { get; }
    }
}
