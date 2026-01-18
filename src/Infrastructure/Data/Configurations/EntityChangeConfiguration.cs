using Domain.Entities.Traceability;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class EntityChangeConfiguration : IEntityTypeConfiguration<EntityChange>
    {
        public void Configure(EntityTypeBuilder<EntityChange> builder)
        {
            builder.ToTable("EntityChange");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("EntityChangeID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Entity)
                .HasColumnName("Entity")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.EntityField)
                .HasColumnName("EntityField")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.FieldType)
                .HasColumnName("FieldType")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            // ⚠️ varchar(50) est souvent trop court pour Old/New, mais je respecte ton DDL
            builder.Property(x => x.OldValue)
                .HasColumnName("OldValue")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.NewValue)
                .HasColumnName("NewValue")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.ChangedAt)
                .HasColumnName("ChangedAt")
                .HasColumnType("datetimeoffset(7)") // si ta DB est datetimeoffset(7), mets "datetimeoffset(7)"
                .IsRequired();

            builder.Property(x => x.ChangedByUserId)
                .HasColumnName("ChangedBy")
                .HasColumnType("nvarchar(450)")
                .IsRequired();

            // ✅ FK vers AspNetUsers sans navigation dans le Domain (shadow navigation)
            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.ChangedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Optionnel : index pour requêtes
            builder.HasIndex(x => x.ChangedAt);
            builder.HasIndex(x => x.ChangedByUserId);
            builder.HasIndex(x => x.Entity);
        }
    }
}
