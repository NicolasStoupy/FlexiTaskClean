using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.MasterData
{
    public sealed class WorkAreaConfiguration : IEntityTypeConfiguration<WorkArea>
    {
        public void Configure(EntityTypeBuilder<WorkArea> builder)
        {
            builder.ToTable("WorkArea");
            builder.Property(x => x.WorkAreaID)
                .HasColumnName("WorkAreaID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
            .HasMaxLength(5)
            .IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.CommonName)
            .HasMaxLength(50)
            .IsRequired();
            builder.HasOne(w => w.WorkAreaType)
                .WithMany()
                .HasForeignKey(w => w.WorkAreaTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            builder.Navigation(x => x.Locations).HasField("_locations");


        }
    }
}