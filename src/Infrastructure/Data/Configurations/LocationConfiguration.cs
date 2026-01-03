using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{

    public sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");

            builder.HasKey(x => x.LocationId);

            builder.Property(x => x.LocationId)
                .HasColumnName("LocationID")
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Label)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();

            builder.HasIndex(x => x.Label).IsUnique();

            builder.Property(x => x.WorkAreaId)
                .HasColumnName("WorkAreaID")
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(x => x.WorkArea)
                .WithMany()
                .HasForeignKey(x => x.WorkAreaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
