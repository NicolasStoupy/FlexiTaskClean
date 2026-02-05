using Domain.Entities.Traceability;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.Traceability
{
    public sealed class EntityChangeConfiguration : IEntityTypeConfiguration<EntityChange>
    {
        public void Configure(EntityTypeBuilder<EntityChange> builder)
        {
            builder.ToTable("EntityChanges");

            // PK
            builder.HasKey(x => x.EntityChangeID);

            builder.Property(x => x.EntityChangeID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Entity)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.EntityField)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.FieldType)
                .HasMaxLength(50)
                .IsRequired();

            // Valeurs avant/après : potentiellement longues
            builder.Property(x => x.OldValue)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.NewValue)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.EntityKey)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ChangedByUserId)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ChangedAt)
                .IsRequired()
                .HasDefaultValueSql("SYSUTCDATETIME()");

            // Index utiles pour l’audit / recherches
            builder.HasIndex(x => new { x.Entity, x.EntityKey });
            builder.HasIndex(x => x.ChangedAt);
            builder.HasIndex(x => x.ChangedByUserId);
        }
    }
}
