using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class WorkAreaConfiguration : IEntityTypeConfiguration<WorkArea>
    {
        public void Configure(EntityTypeBuilder<WorkArea> builder)
        {
            builder.ToTable("WorkArea");

            builder.HasKey(x => x.WorkAreaId);

            builder.Property(x => x.WorkAreaId)
                .HasColumnName("WorkAreaID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
            .HasMaxLength(5)
            .IsRequired();

            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.CommonName)
            .HasMaxLength(50)
            .IsRequired();

            //builder.Property(x => x.PlantId)
            //.HasColumnName("PlantID")
            //.IsRequired();

            //builder.Property(x => x.WorkAreaTypeId)
            //    .HasColumnName("WorkAreaTypeID")
            //    .IsRequired();

            builder.Property(x => x.Created)
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100);

            builder.Property(x => x.LastModified)
                   .IsRequired();

            builder.Property(x => x.LastModifiedBy)
                   .HasMaxLength(100);
        }
    }
}