using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.MasterData
{
    public sealed class PlantIdentityConfiguration : IEntityTypeConfiguration<PlantIdentity>
    {
        public void Configure(EntityTypeBuilder<PlantIdentity> builder)
        {
            builder.ToTable("PlantIdentity"); // oui, le nom est exactement celui du SQL

            builder.HasKey(x => new { x.PlantID, x.AspNetIdentityID });

            builder.Property(x => x.PlantID)
                .HasColumnName("PlantID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.AspNetIdentityID)
                .HasColumnType("nvarchar(450)")
                .HasMaxLength(450)
                .IsUnicode(true)
                .IsRequired();
        }
    }
}