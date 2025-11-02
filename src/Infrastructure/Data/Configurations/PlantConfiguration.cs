using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PlantConfiguration : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.ToTable("Plant");          
            builder.HasKey(t => t.Id);                
            builder.Property(t => t.Id)
                   .HasColumnName("PlantID");
            builder.Property(t => t.Code)
                   .HasMaxLength(4)
                   .IsRequired();
        }
    }
}
