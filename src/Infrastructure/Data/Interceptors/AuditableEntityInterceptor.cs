using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Interceptors
{
    /// <summary>
    /// Intercepteur Entity Framework Core chargé de gérer automatiquement
    /// les propriétés d’audit (<see cref="BaseAuditableEntity"/>) lors des opérations
    /// de sauvegarde du contexte de données.
    /// </summary>
    /// <remarks>
    /// Cet intercepteur met à jour les champs <c>Created</c>, <c>CreatedBy</c>,
    /// <c>LastModified</c> et <c>LastModifiedBy</c> avant l’enregistrement des entités.
    /// Il s’appuie sur une implémentation de <see cref="IUser"/> pour identifier l’utilisateur actif,
    /// et sur un <see cref="TimeProvider"/> pour récupérer la date et l’heure actuelles (en UTC).
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly IUser _user;
        private readonly TimeProvider _dateTime;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="AuditableEntityInterceptor"/>.
        /// </summary>
        /// <param name="user">Le service fournissant l’identifiant de l’utilisateur actif.</param>
        /// <param name="dateTime">Le fournisseur de temps utilisé pour récupérer l’heure actuelle (UTC).</param>
        public AuditableEntityInterceptor(
            IUser user,
            TimeProvider dateTime)
        {
            _user = user;
            _dateTime = dateTime;
        }

        /// <summary>
        /// S’exécute lors d’un appel synchrone à <see cref="DbContext.SaveChanges"/>.
        /// Met à jour les métadonnées d’audit avant la sauvegarde.
        /// </summary>
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        /// <summary>
        /// S’exécute lors d’un appel asynchrone à <see cref="DbContext.SaveChangesAsync"/>.
        /// Met à jour les métadonnées d’audit avant la sauvegarde.
        /// </summary>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Met à jour les propriétés d’audit pour toutes les entités suivies
        /// héritant de <see cref="BaseAuditableEntity"/>.
        /// </summary>
        /// <param name="context">Le contexte EF Core actuel.</param>
        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    var utcNow = _dateTime.GetUtcNow();
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedBy = _user.Id;
                        entry.Entity.Created = utcNow;
                    }
                    entry.Entity.LastModifiedBy = _user.Id;
                    entry.Entity.LastModified = utcNow;
                }
            }
        }
    }

    /// <summary>
    /// Fournit des méthodes d’extension pour les entrées d’entités EF Core.
    /// </summary>
    public static class Extensions
    {  /// <summary>
       /// Détermine si une entité possède des entités « owned » ayant été ajoutées ou modifiées.
       /// </summary>
       /// <param name="entry">L’entrée EF Core à analyser.</param>
       /// <returns>
       /// <see langword="true"/> si au moins une entité détenue (owned entity)
       /// a été ajoutée ou modifiée ; sinon <see langword="false"/>.
       /// </returns>
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}