using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.MasterData
{
    public sealed class SupportTypeConfiguration : IEntityTypeConfiguration<SupportType>
    {
        public void Configure(EntityTypeBuilder<SupportType> builder)
        {
            builder.ToTable("SupportType");

            builder.HasKey(x => x.SupportTypeID);

            builder.Property(x => x.SupportTypeID)
                   .HasMaxLength(10)
                   .IsRequired();

            // SQL float => double
            builder.Property(x => x.MaxLoad).IsRequired();

            builder.Property(x => x.Active).IsRequired();

            // Description est varchar(max) dans ta DB (nullable ou non selon ton script)
            builder.Property(x => x.Description);
        }
    }
}
