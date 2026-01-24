using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Interceptors
{
    /// <summary>
    /// Intercepteur EF Core qui dispatch les événements de domaine présents sur les entités
    /// avant la persistance (SaveChanges / SaveChangesAsync).
    /// </summary>
    public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Crée une instance de <see cref="DispatchDomainEventsInterceptor"/>.
        /// </summary>
        /// <param name="mediator">Instance de MediatR utilisée pour publier les événements de domaine.</param>
        public DispatchDomainEventsInterceptor(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Méthode appelée synchronously avant l'exécution de <c>SaveChanges</c>.
        /// Elle déclenche l'envoi des événements de domaine collectés sur le contexte courant.
        /// </summary>
        /// <param name="eventData">Données de l'événement de contexte fournies par EF Core.</param>
        /// <param name="result">Résultat d'interception précédent.</param>
        /// <returns>L'<see cref="InterceptionResult{Int32}"/> éventuellement modifié.</returns>
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

            return base.SavingChanges(eventData, result);

        }

        /// <summary>
        /// Méthode appelée asynchronously avant l'exécution de <c>SaveChangesAsync</c>.
        /// Elle attend l'envoi des événements de domaine collectés sur le contexte courant.
        /// </summary>
        /// <param name="eventData">Données de l'événement de contexte fournies par EF Core.</param>
        /// <param name="result">Résultat d'interception précédent.</param>
        /// <param name="cancellationToken">Jeton d'annulation optionnel pour l'opération asynchrone.</param>
        /// <returns>Un <see cref="ValueTask{InterceptionResult{Int32}}"/> représentant l'opération d'interception.</returns>
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Récupère tous les événements de domaine présents sur les entités suivies par le contexte,
        /// publie chaque événement via MediatR puis vide la collection d'événements sur chaque entité.
        /// </summary>
        /// <param name="context">Le <see cref="DbContext"/> courant ; si null la méthode retourne immédiatement.</param>
        /// <returns>Une tâche représentant l'opération de publication des événements.</returns>
        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;

            var entities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);
        }
    }
}
