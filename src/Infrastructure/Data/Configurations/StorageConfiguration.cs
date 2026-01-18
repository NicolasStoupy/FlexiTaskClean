using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class StorageConfiguration : IEntityTypeConfiguration<Domain.Entities.Inventory.Storage>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Inventory.Storage> builder)
        {
            builder.ToTable("Storage");

            builder.HasKey(x => new { x.LocationId, x.StorageId });

            builder.Property(x => x.LocationId)
                .HasColumnName("LocationID")
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.StorageId)
                .HasColumnName("StorageID")
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.LengthInMillimeters)
                .HasColumnType("float")
                .IsRequired();

            builder.Property(x => x.Empty)
                .HasColumnType("bit")
                .IsRequired();

      
        }
    }
}
