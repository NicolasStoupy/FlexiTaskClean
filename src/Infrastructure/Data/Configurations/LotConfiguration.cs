using Domain.Entities.Inventory;

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
                .HasColumnType("int");

            builder.Property(x => x.ProductId)
                .HasColumnName("ProductID")
                .HasColumnType("int")
                .IsRequired();

            // FK vers Storage (composite)
            builder.HasOne(x => x.Storage)
                .WithMany(s => s.Lots)
                .HasForeignKey(x => new { x.LocationId, x.StorageId })
                .OnDelete(DeleteBehavior.NoAction);

            // FK vers Product (simple)
            builder.HasOne(x => x.Product)
                .WithMany(p => p.Lots)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
