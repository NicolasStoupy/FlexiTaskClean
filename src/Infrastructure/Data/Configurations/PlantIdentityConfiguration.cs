using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
   public sealed class PlantIdentityConfiguration : IEntityTypeConfiguration<PlantIdentity>
{
    public void Configure(EntityTypeBuilder<PlantIdentity> builder)
    {
        builder.ToTable("PlantIDentity"); // oui, le nom est exactement celui du SQL

        builder.HasKey(x => new { x.PlantId, x.Id_AspnetIdentity });

        builder.Property(x => x.PlantId)
            .HasColumnName("PlantID")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.Id_AspnetIdentity)
            .HasColumnType("nvarchar(450)")
            .HasMaxLength(450)
            .IsUnicode(true)
            .IsRequired();

        builder.HasOne(x => x.Plant)
            .WithMany()
            .HasForeignKey(x => x.PlantId)
            .OnDelete(DeleteBehavior.NoAction);

      
    }
}
}
