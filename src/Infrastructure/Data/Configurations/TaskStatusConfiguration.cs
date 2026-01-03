using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class TaskStatusConfiguration : IEntityTypeConfiguration<Domain.Entities.Tasks.TaskStatus>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Tasks.TaskStatus> builder)
        {
            builder.ToTable("TaskStatus");

            builder.HasKey(x => x.TaskStatusId);

            builder.Property(x => x.TaskStatusId)
                .HasColumnName("TaskStatusID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
                .HasColumnType("varchar(5)")
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsRequired();

            builder.HasIndex(x => x.Code).IsUnique();

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
