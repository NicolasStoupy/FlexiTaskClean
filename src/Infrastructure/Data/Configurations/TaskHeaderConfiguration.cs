using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class TaskHeaderConfiguration : IEntityTypeConfiguration<TaskHeader>
    {
        public void Configure(EntityTypeBuilder<TaskHeader> builder)
        {
            builder.ToTable("TaskHeader");

            builder.HasKey(x => x.TaskHeaderId);

            builder.Property(x => x.TaskHeaderId)
                .HasColumnName("TaskHeaderID")
                .ValueGeneratedOnAdd();

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
