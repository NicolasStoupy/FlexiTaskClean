using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class TaskItemTypeConfiguration : IEntityTypeConfiguration<TaskItemType>
    {
        public void Configure(EntityTypeBuilder<TaskItemType> builder)
        {
            builder.ToTable("TaskItemType");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("TaskItemType_ID")
                .HasColumnType("varchar(4)")
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.InstructionDescription)
                .HasColumnName("InstructionDescription")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.JsonSchemaTemplateId)
                .HasColumnName("JsonSchemaTemplateID")
                .HasColumnType("int")
                .IsRequired();          

            builder.HasOne(x => x.JsonSchemaTemplate)
                .WithMany()
                .HasForeignKey(x => x.JsonSchemaTemplateId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
