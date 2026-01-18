using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class WorkAreaTypeConfiguration : IEntityTypeConfiguration<WorkAreaType>
    {
        public void Configure(EntityTypeBuilder<WorkAreaType> builder)
        {
            builder.ToTable("WorkAreaType");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)                
                .ValueGeneratedOnAdd();
           

        }
    }
}
