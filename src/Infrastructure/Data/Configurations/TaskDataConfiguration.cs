using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class TaskDataConfiguration : IEntityTypeConfiguration<TaskData>
    {
        public void Configure(EntityTypeBuilder<TaskData> builder)
        {
            builder.ToTable("TaskData");

            builder.HasKey(x => x.TaskDataId);

            builder.Property(x => x.TaskDataId)
                .HasColumnName("TaskDataID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.JsonData)
                .HasColumnType("varchar(max)")
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.JsonSchemaTemplateId)
                .HasColumnName("JsonSchemaTemplateID")
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

            builder.HasOne(x => x.JsonSchemaTemplate)
                .WithMany()
                .HasForeignKey(x => x.JsonSchemaTemplateId)
                .OnDelete(DeleteBehavior.NoAction);

            // CHECK (isjson([JsonData]) = 1)
            builder.ToTable(t => t.HasCheckConstraint("CK_JsonTaskData_ValidJson", "isjson([JsonData])=(1)"));
        }
    }
}
