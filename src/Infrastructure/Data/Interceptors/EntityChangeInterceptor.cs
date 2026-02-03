using Application.Common.Interfaces;
using Domain.Entities.Traceability;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Data.Interceptors
{
    public class EntityChangeInterceptor : SaveChangesInterceptor
    {

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeProvider _time;

        private static readonly HashSet<string> IgnoredFields = new(StringComparer.OrdinalIgnoreCase)
        {
        "Created", "CreatedBy", "LastModified", "LastModifiedBy", "RowVersion"
        };

        public EntityChangeInterceptor(IServiceScopeFactory scopeFactory, TimeProvider time)
        {
            _scopeFactory = scopeFactory;
            _time = time;
        }


        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            AddEntityChanges(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            AddEntityChanges(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AddEntityChanges(DbContext? context)
        {
            try
            {
                if (context is null) return;

                //  ne pas auditer EntityChange (sinon boucle)
                if (context.ChangeTracker.Entries<EntityChange>().Any(e => e.State != EntityState.Unchanged))
                    return;

                using var scope = _scopeFactory.CreateScope();
                var user = scope.ServiceProvider.GetService<IUser>();
                var userId = user?.Id ?? "system";
                var now = _time.GetUtcNow();

                var changes = new List<EntityChange>();

                foreach (var entry in context.ChangeTracker.Entries())
                {
                    var entityKey = BuildEntityKeyJson(entry);

                    if (entry.State is not (EntityState.Modified or EntityState.Added or EntityState.Deleted))
                        continue;

                    // Ignore EntityChange entity itself
                    if (entry.Entity is EntityChange)
                        continue;

                    // Option: ignore owned types (ou traite-les séparément)
                    if (entry.Metadata.IsOwned())
                        continue;

                    var entityName = entry.Metadata.ClrType.Name;

                    // MODIFIED : Old/New par propriété modifiée
                    if (entry.State == EntityState.Modified)
                    {
                        foreach (var prop in entry.Properties.Where(p => p.IsModified))
                        {
                            var field = prop.Metadata.Name;
                            if (IgnoredFields.Contains(field)) continue;

                            var oldVal = ToStringSafe(prop.OriginalValue);
                            var newVal = ToStringSafe(prop.CurrentValue);

                            if (oldVal == newVal) continue;

                            changes.Add(new EntityChange
                            {
                                Entity = entityName,
                                EntityField = field,
                                FieldType = prop.Metadata.ClrType.Name,
                                OldValue = oldVal,
                                NewValue = newVal,
                                ChangedAt = now,
                                ChangedByUserId = userId,
                                EntityKey = entityKey

                            });
                        }
                    }
                    else
                    {
                        // ADDED / DELETED 
                        changes.Add(new EntityChange
                        {
                            Entity = entityName,
                            EntityField = "__STATE__",
                            FieldType = "string",
                            OldValue = entry.State == EntityState.Added ? null : "exists",
                            NewValue = entry.State == EntityState.Deleted ? null : "created",
                            ChangedAt = now,
                            ChangedByUserId = userId,
                            EntityKey = entityKey

                        });
                    }
                }

                if (changes.Count > 0)
                {
                    context.Set<EntityChange>().AddRange(changes);
                }
            }
            catch (Exception ex )
            {

                throw;
            }
           
        }

        private static string? ToStringSafe(object? value)
            => value switch
            {
                null => null,
                DateTime dt => dt.ToUniversalTime().ToString("O"),
                DateTimeOffset dto => dto.ToUniversalTime().ToString("O"),
                _ => value.ToString()
            };

        private static string BuildEntityKeyJson(EntityEntry entry)
        {
            var pk = entry.Metadata.FindPrimaryKey();
            if (pk is null) return "{}";

            var dict = new Dictionary<string, object?>();
            foreach (var p in pk.Properties)
            {
                var val = entry.Property(p.Name).CurrentValue ?? entry.Property(p.Name).OriginalValue;
                dict[p.Name] = val;
            }

            return JsonSerializer.Serialize(dict);
        }
    }
}
