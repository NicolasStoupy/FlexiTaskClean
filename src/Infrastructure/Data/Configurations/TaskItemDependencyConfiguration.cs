using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class TaskItemDependencyConfiguration : IEntityTypeConfiguration<TaskItemDependency>
    {
        public void Configure(EntityTypeBuilder<TaskItemDependency> builder)
        {
            builder.ToTable("TaskItemDependency");

            builder.HasKey(x => new
            {
                x.TaskHeaderIdDependOn,
                x.TaskItemsIdDependOn,
                x.TaskHeaderId,
                x.TaskItemsId
            });

            builder.Property(x => x.TaskHeaderIdDependOn).HasColumnName("TaskHeaderID_DependOn").HasColumnType("int");
            builder.Property(x => x.TaskItemsIdDependOn).HasColumnName("TaskItemsID_DependOn").HasColumnType("int");
            builder.Property(x => x.TaskHeaderId).HasColumnName("TaskHeaderID").HasColumnType("int");
            builder.Property(x => x.TaskItemsId).HasColumnName("TaskItemsID").HasColumnType("int");

            // FK -> TaskItems (DependOn)
            builder.HasOne(x => x.DependOnTaskItem)
                .WithMany()
                .HasForeignKey(x => new { x.TaskHeaderIdDependOn, x.TaskItemsIdDependOn })
                .OnDelete(DeleteBehavior.NoAction);

            // FK -> TaskItems (Target)
            builder.HasOne(x => x.TaskItem)
                .WithMany()
                .HasForeignKey(x => new { x.TaskHeaderId, x.TaskItemsId })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
