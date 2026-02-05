using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    
    public abstract class BaseEntity
    {

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

    public abstract class BaseEntity<TId>
    {
        //public TId Id { get; set; } = default!;

        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
    //public abstract class BaseEntity
    //{       

    //    private readonly List<BaseEvent> _domainEvents = new();

    //    [NotMapped]
    //    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    //    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
    //    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
    //    public void ClearDomainEvents() => _domainEvents.Clear();
    //}
}
