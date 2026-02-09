using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.MasterData
{
    public sealed class WorkAreaTransportSupportConfiguration : IEntityTypeConfiguration<WorkAreaTransportSupport>
    {
        public void Configure(EntityTypeBuilder<WorkAreaTransportSupport> builder)
        {
            builder.ToTable("WA_TransportSupport");

            builder.HasKey(x => new { x.WorkAreaID, x.SupportTypeID });

            builder.Property(x => x.SupportTypeID)
                   .HasMaxLength(10)
                   .IsRequired();

            
       
        }
    }
}
