using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class WorkAreaConfiguration:IEntityTypeConfiguration<Domain.Entities.WorkArea>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.WorkArea> builder)
        {
            builder.HasKey(wa => wa.WorkAreaId);
            builder.Property(wa => wa.Code)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(wa => wa.CommonName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
