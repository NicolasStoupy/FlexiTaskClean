using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Interceptors
{
    /// <summary>
    /// Intercepteur Entity Framework Core chargé de gérer automatiquement
    /// les propriétés d’audit (<see cref="IAuditableEntity"/>) lors des opérations
    /// de sauvegarde du contexte de données.
    /// </summary>
    /// <remarks>
    /// Cet intercepteur met à jour les champs <c>Created</c>, <c>CreatedBy</c>,
    /// <c>LastModified</c> et <c>LastModifiedBy</c> avant l’enregistrement des entités.
    /// 
    /// IMPORTANT (DbContextFactory):
    /// - L'interceptor est enregistré en Singleton.
    /// - Il ne peut donc pas dépendre directement d'un service Scoped (ex: <see cref="IUser"/>).
    /// - On résout <see cref="IUser"/> via un scope à chaque SaveChanges.
    /// </remarks>
    public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeProvider _dateTime;

        public AuditableEntityInterceptor(
            IServiceScopeFactory scopeFactory,
            TimeProvider dateTime)
        {
            _scopeFactory = scopeFactory;
            _dateTime = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context is null) return;

            // Résolution "scoped" de IUser au moment du SaveChanges
            using var scope = _scopeFactory.CreateScope();
            var user = scope.ServiceProvider.GetService<IUser>();

            // Fallback si pas d'utilisateur (batch, job, contexte technique, etc.)
            var userId = user?.Id ?? "system";
            var utcNow = _dateTime.GetUtcNow();

            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.Created = utcNow;
                    }

                    entry.Entity.LastModifiedBy = userId;
                    entry.Entity.LastModified = utcNow;
                }
            }
        }
    }

    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
