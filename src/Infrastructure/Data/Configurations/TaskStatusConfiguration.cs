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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("TaskStatusID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasColumnType("varchar(5)")
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            // Contrainte UNIQUE(Code)
            builder.HasIndex(x => x.Code)
                   .IsUnique();

            // Audit
            builder.Property(x => x.Created)
                .HasColumnType("datetimeoffset(7)");

            builder.Property(x => x.CreatedBy)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.LastModified)
                .HasColumnType("datetimeoffset(7)");

            builder.Property(x => x.LastModifiedBy)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
