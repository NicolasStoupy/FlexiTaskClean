using Domain.Entities.MasterData;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PlantConfiguration : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.ToTable("Plant");

            builder.Property(x => x.Id)
                .HasColumnName("PlantID");

            builder.Property(x => x.Code)
                .HasMaxLength(4);

            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.CommonName)
                .HasMaxLength(50);

            builder.Property(x => x.Language)
                .HasColumnName("LanguageEnum")
                .HasMaxLength(50)               
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<PlantLanguage>(v, ignoreCase: true));
        }
    }
}
