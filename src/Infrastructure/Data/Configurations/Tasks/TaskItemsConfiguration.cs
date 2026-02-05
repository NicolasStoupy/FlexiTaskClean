using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.Tasks
{
    public sealed class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.UseTptMappingStrategy().ToTable("TaskItem");
            builder.HasKey(x => new { x.TaskHeaderID, x.TaskItemID }); // Composite Key

            builder.Property(x => x.TaskItemID)
                .ValueGeneratedOnAdd(); // Auto-increment TaskItemID within the scope of TaskHeaderID

            builder.Property(x => x.TaskItemStatus).HasConversion<int>();
            builder.Property(x => x.LinkedWorkArea).IsRequired();

         


            builder.HasOne<TaskHeader>()
                .WithMany(th => th.TaskItems)
                .HasForeignKey(ti => ti.TaskHeaderID); // Foreign key relationship

            builder.Property(x => x.TaskItemStatus).HasConversion<int>()
                .HasMaxLength(50);

            builder.HasMany(t => t.Prerequisites)
                .WithOne(d => d.TaskItem)
                .HasForeignKey(d => new { d.TaskHeaderID, d.TaskItemID })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.NextSteps)
                .WithOne(d => d.DependsOn)
                .HasForeignKey(d => new { d.DependsOnTaskHeaderID, d.DependsOnTaskItemID })
                .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(ti => ti.Prerequisites).HasField("_prerequisites");
            builder.Navigation(ti => ti.NextSteps).HasField("_nextSteps");
        }
    }
}