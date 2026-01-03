using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class WorkAreaTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.WorkAreaType>
    {
        public void Configure(EntityTypeBuilder<WorkAreaType> builder)
        {
            builder.ToTable("WorkAreaType");
            builder.HasKey(x => x.WorkAreaTypeId);
            builder.Property(x => x.WorkAreaTypeId)
                .HasColumnName("WorkAreaTypeID")
                .ValueGeneratedOnAdd();
           

        }
    }
}
