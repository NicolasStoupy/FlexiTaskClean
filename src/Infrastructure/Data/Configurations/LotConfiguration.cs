using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.ToTable("Lot");

            builder.HasKey(x => new { x.LocationId, x.StorageId, x.LotId });

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

            builder.Property(x => x.LotId)
                .HasColumnName("LotID")
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Blocked)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.PositionStorage)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(x => x.ProductId)
                .HasColumnName("ProductID")
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(x => x.Storage)
                .WithMany()
                .HasForeignKey(x => new { x.LocationId, x.StorageId })
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
