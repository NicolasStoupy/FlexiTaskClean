using Domain.Entities.MasterData;
using Domain.Enums;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.Data.Configurations.MasterData
{
    public class PlantConfiguration : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.ToTable("Plant");

            builder.HasKey(p => p.PlantID);

            builder.Property(x => x.Code)
                .HasMaxLength(4);

            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.CommonName)
                .HasMaxLength(50);

            builder.Property(x => x.Language).HasConversion<string>()
                .HasMaxLength(50);

            builder.HasMany(p => p.WorkAreas)
                .WithOne()
                .HasForeignKey(p=>p.PlantID)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.HasMany<ApplicationUser>("Users")   // navigation shadow côté Plant
            //   .WithMany("Plants")                  // navigation shadow côté User (optionnelle)
            //   .UsingEntity<Dictionary<string, object>>(
            //       "PlantIdentity",
            //       j => j.HasOne<ApplicationUser>()
            //             .WithMany()
            //             .HasForeignKey("AspNetIdentityID")
            //             .HasPrincipalKey(u => u.Id)          // PK string de Identity
            //             .OnDelete(DeleteBehavior.NoAction),
            //       j => j.HasOne<Plant>()
            //             .WithMany()
            //             .HasForeignKey("PlantID")
            //             .HasPrincipalKey(p => p.PlantID)     // PK int de Plant
            //             .OnDelete(DeleteBehavior.NoAction),
            //       j =>
            //       {
            //           j.ToTable("PlantIdentity");
            //           j.HasKey("PlantID", "AspNetIdentityID");
            //       });

            //builder.Navigation(p => p.PlantIdentities).HasField("_plantIdentities");
            builder.Navigation(p => p.WorkAreas).HasField("_workAreas");


        }
    }
}