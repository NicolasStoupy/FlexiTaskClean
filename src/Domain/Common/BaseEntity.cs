using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    /// <summary>
    /// Classe de base abstraite pour toutes les entités du domaine.
    /// </summary>
    /// <remarks>
    /// Cette classe fournit un identifiant unique et la gestion des événements de domaine,
    /// conformément aux principes du Domain-Driven Design (DDD).  
    /// Les entités héritant de <see cref="BaseEntity"/> peuvent ainsi émettre des événements
    /// (<see cref="BaseEvent"/>) afin de notifier d'autres composants du système
    /// (ex. : envoi d'email, mise à jour de projections, intégration externe, etc.).
    /// </remarks>  
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identifiant unique de l’entité.
        /// </summary>
        public int Id { get; set; }

        private readonly List<BaseEvent> _domainEvents = new();

        /// <summary>
        /// Liste en lecture seule des événements de domaine associés à cette entité.
        /// </summary>
        /// <remarks>
        /// Cette propriété est marquée avec <see cref="NotMappedAttribute"/> afin
        /// qu’elle ne soit pas persistée dans la base de données par Entity Framework.
        /// </remarks>
        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Ajoute un événement de domaine à la liste des événements associés à cette entité.
        /// </summary>
        /// <param name="domainEvent">L’événement de domaine à ajouter.</param>
        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Supprime un événement de domaine de la liste des événements associés à cette entité.
        /// </summary>
        /// <param name="domainEvent">L’événement de domaine à supprimer.</param>
        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Supprime tous les événements de domaine associés à cette entité.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

}
