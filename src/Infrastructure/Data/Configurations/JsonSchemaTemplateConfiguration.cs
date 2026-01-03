using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class JsonSchemaTemplateConfiguration : IEntityTypeConfiguration<JsonSchemaTemplate>
    {
        public void Configure(EntityTypeBuilder<JsonSchemaTemplate> builder)
        {
            builder.ToTable("JsonSchemaTemplate");

            builder.HasKey(x => x.JsonSchemaTemplateId);

            builder.Property(x => x.JsonSchemaTemplateId)
                .HasColumnName("JsonSchemaTemplateID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Label)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.JsonSchema)
                .HasColumnType("varchar(max)")
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Version)
                .HasColumnType("int")
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

            // CHECK (isjson([JsonSchema]) = 1)
            builder.ToTable(t => t.HasCheckConstraint("CK_JsonSchemaTemplate_ValidJson", "isjson([JsonSchema])=(1)"));
        }
    }
}
