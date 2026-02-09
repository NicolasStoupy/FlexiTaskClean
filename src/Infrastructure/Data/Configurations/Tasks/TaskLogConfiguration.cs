using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.Tasks
{
    public class TaskLogConfiguration : IEntityTypeConfiguration<TaskLog>
    {
        public void Configure(EntityTypeBuilder<TaskLog> builder)
        {
            builder.ToTable("TaskLog");
            builder.HasKey(x => x.TaskLogID);

            builder.Property(x => x.EventType)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.PerformedBy)
                .HasMaxLength(100);

            builder.Property(x => x.Comment)
                .HasMaxLength(500);

            builder.HasIndex(x => x.TaskItemID);
            builder.HasIndex(x => x.TaskHeaderID);
        }
    }

}
