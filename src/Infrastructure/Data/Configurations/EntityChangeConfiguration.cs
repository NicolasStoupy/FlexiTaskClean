using Domain.Entities.Logging;
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

            builder.HasKey(x => x.EntityChangeId);

            builder.Property(x => x.EntityChangeId)
                .HasColumnName("EntityChangeID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Entity)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.EntityField)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.FieldType)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.OldValue)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.NewValue)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.ChangedAt)                
                .IsRequired(false);

            builder.Property(x => x.ChangedBy)
                .HasColumnType("nvarchar(450)")
                .HasMaxLength(450)
                .IsUnicode(true)
                .IsRequired();

            //// FK -> AspNetUsers(Id)
            //builder.HasOne(x => x.ChangedBy)
            //    .WithMany()
            //    .HasForeignKey(x => x.ChangedBy)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
