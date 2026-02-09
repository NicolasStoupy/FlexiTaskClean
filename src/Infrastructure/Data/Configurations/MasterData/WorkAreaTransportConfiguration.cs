using Domain.Entities.MasterData;
using Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.MasterData
{
    public class WorkAreaTransportConfiguration : IEntityTypeConfiguration<WorkAreaTransport>
    {
        public void Configure(EntityTypeBuilder<WorkAreaTransport> builder)
        {
            builder.ToTable("WA_Transport");
            builder.HasBaseType<WorkArea>();

            builder.Property(x => x.TruckName).IsRequired();
            builder.Property(x => x.MaxLoad).IsRequired();

            // ✅ SupportedTypes = vraie nav many-to-many (CRUD direct)
            builder.HasMany(x => x.SupportedTypes)
                   .WithMany()
                   .UsingEntity<WorkAreaTransportSupport>(
                        j => j.HasOne<SupportType>()
                              .WithMany()
                              .HasForeignKey(x => x.SupportTypeID)
                              .HasPrincipalKey(st => st.SupportTypeID)
                              .OnDelete(DeleteBehavior.Restrict),
                        j => j.HasOne<WorkAreaTransport>()
                              .WithMany()
                              .HasForeignKey(x => x.WorkAreaID)
                              .HasPrincipalKey(t => t.WorkAreaID)   // clé héritée (WorkAreaID)
                              .OnDelete(DeleteBehavior.Restrict),
                        j =>
                        {
                            j.ToTable("WA_TransportSupport");
                            j.HasKey(x => new { x.WorkAreaID, x.SupportTypeID });

                            // ✅ impose les noms exacts de colonnes
                            j.Property(x => x.WorkAreaID).HasColumnName("WorkAreaID");
                            j.Property(x => x.SupportTypeID).HasColumnName("SupportTypeID").HasMaxLength(10);
                        });

            builder.Navigation(x => x.SupportedTypes).AutoInclude();
            builder.Navigation(x => x.SupportedTypes)
                .HasField("_supportedTypes")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
