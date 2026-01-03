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

            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.ProductId)
                .HasColumnName("ProductID")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(x => x.Description)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Created)              
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(false);

            builder.Property(x => x.LastModified)             
                .IsRequired();

            builder.Property(x => x.LastModifiedBy)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(false);
        }
    }
}
