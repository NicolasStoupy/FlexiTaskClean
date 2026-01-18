using Domain.Entities.DynamicForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class JsonSchemaTemplateConfiguration : IEntityTypeConfiguration<JsonSchemaTemplate>
    {
        public void Configure(EntityTypeBuilder<JsonSchemaTemplate> builder)
        {
            builder.ToTable("JsonSchemaTemplate", t =>
            {
                t.HasCheckConstraint(
                    "CK_JsonSchemaTemplate_ValidJson",
                    "ISJSON([JsonSchema]) = 1"
                );
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("JsonSchemaTemplateID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Label)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.JsonSchema)
                .HasColumnType("nvarchar(max)")   // 👈 idéal
                .IsRequired();

            builder.Property(x => x.Version)
                .IsRequired();

            // Optionnel mais conseillé
            builder.HasIndex(x => new { x.Label, x.Version })
                   .IsUnique();
        }
    }
}
