using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("TaskItems");

            builder.HasKey(x => new { x.TaskHeaderId, x.TaskItemId });

            builder.Property(x => x.TaskHeaderId).HasColumnName("TaskHeaderID");

            builder.Property(x => x.TaskItemId)
                .HasColumnName("TaskItemsID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.LinkedWorkAreaId).HasColumnName("LinkedWorkArea");

            builder.HasOne(x => x.TaskHeader)
                .WithMany(h => h.Items)
                .HasForeignKey(x => x.TaskHeaderId)
                .OnDelete(DeleteBehavior.NoAction);          

            builder.HasOne(x => x.LinkedWorkArea)
                .WithMany()
                .HasForeignKey(x => x.LinkedWorkAreaId)
                .OnDelete(DeleteBehavior.NoAction);

            // Dependencies (self ref via join entity)
            builder.HasMany(x => x.Dependencies)
                .WithOne(d => d.TaskItem)
                .HasForeignKey(d => new { d.TaskHeaderId, d.TaskItemId })
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.DependentBy)
                .WithOne(d => d.DependsOn)
                .HasForeignKey(d => new { d.DependsOnTaskHeaderId, d.DependsOnTaskItemId })
                .OnDelete(DeleteBehavior.NoAction);

            // Extensions 1-1
            builder.HasOne(x => x.TransportTask)
                .WithOne(t => t.TaskItem)
                .HasForeignKey<TransportTask>(t => new { t.TaskHeaderId, t.TaskItemId })
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.LoadingTask)
                .WithOne(t => t.TaskItem)
                .HasForeignKey<LoadingTask>(t => new { t.TaskHeaderId, t.TaskItemId })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

