using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ProductID")
                .ValueGeneratedNever(); // si la DB ne génère pas (à adapter)

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
        }

    }
}
