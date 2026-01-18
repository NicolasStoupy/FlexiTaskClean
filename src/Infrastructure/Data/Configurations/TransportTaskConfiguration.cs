using Domain.Entities.Tasks.TaskSpecializations;
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

            // 👉 ICI la composite PK
            builder.HasKey(x => new { x.TaskHeaderId, x.TaskItemId });

            builder.Property(x => x.TaskHeaderId)
                .HasColumnName("TaskHeaderID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.TaskItemId)
                .HasColumnName("TaskItemsID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Support)
                .HasColumnName("Support")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(x => x.SourceAreaId)
                .HasColumnName("SourceArea")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.DestinationAreaId)
                .HasColumnName("DestinationArea")
                .HasColumnType("int")
                .IsRequired();

            // 👉 1–1 avec TaskItem via la MÊME PK composite
            builder.HasOne(x => x.TaskItem)
                .WithOne(t => t.TransportTask)
                .HasForeignKey<TransportTask>(x => new { x.TaskHeaderId, x.TaskItemId })
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.SourceArea)
                .WithMany()
                .HasForeignKey(x => x.SourceAreaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.DestinationArea)
                .WithMany()
                .HasForeignKey(x => x.DestinationAreaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
