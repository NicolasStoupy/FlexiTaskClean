using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class TransportTaskConfiguration : IEntityTypeConfiguration<TransportTask>
    {
        public void Configure(EntityTypeBuilder<TransportTask> builder)
        {
            builder.ToTable("TransportTask");

            builder.HasKey(x => new { x.TaskHeaderId, x.TaskItemsId });

            builder.Property(x => x.TaskHeaderId)
                .HasColumnName("TaskHeaderID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.TaskItemsId)
                .HasColumnName("TaskItemsID")
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(x => x.TaskItem)
                .WithOne(ti => ti.TransportTask)
                .HasForeignKey<TransportTask>(x => new { x.TaskHeaderId, x.TaskItemsId })
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.DestinationArea).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SourceArea).HasColumnType("int").IsRequired(false);

            builder.HasOne(x => x.DestinationWorkArea)
                .WithMany()
                .HasForeignKey(x => x.DestinationArea)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.SourceWorkArea)
                .WithMany()
                .HasForeignKey(x => x.SourceArea)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
