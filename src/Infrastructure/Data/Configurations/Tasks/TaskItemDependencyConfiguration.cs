using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.Tasks
{
    public sealed class TaskItemDependencyConfiguration : IEntityTypeConfiguration<TaskItemDependency>
    {
        public void Configure(EntityTypeBuilder<TaskItemDependency> builder)
        {

            builder.HasKey(x => new
            {
                x.TaskHeaderID,
                x.TaskItemID,
                x.DependsOnTaskHeaderID,
                x.DependsOnTaskItemID
            });

            builder.HasOne(x => x.TaskItem)
                .WithMany(t => t.Prerequisites)
                .HasForeignKey(x => new { x.TaskHeaderID, x.TaskItemID })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DependsOn)
                .WithMany(t => t.NextSteps)
                .HasForeignKey(x => new { x.DependsOnTaskHeaderID, x.DependsOnTaskItemID })
                .OnDelete(DeleteBehavior.Restrict);


            // Audit (si tu veux mapper explicitement datetimeoffset(7)/varchar(50))
            builder.Property(x => x.Created).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.LastModified).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.LastModifiedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
        }
    }
}
