using Domain.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.MasterData
{

    public sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");

            builder.HasKey(x => x.LocationID);

            builder.Property(x => x.LocationID)
                .HasMaxLength(50)           
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.Label)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(x => x.WorkArea)
                .WithMany(wa => wa.Locations)   // si tu as WorkArea.Locations
                .HasForeignKey(x => x.WorkAreaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.WorkAreaId);
            builder.HasIndex(x => new { x.WorkAreaId, x.LocationID }).IsUnique(false);
          
        }
    }
}
