using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.Tasks.TaskSpecializations
{
    public sealed class TransportTaskConfiguration : IEntityTypeConfiguration<TransportTask>
    {
        public void Configure(EntityTypeBuilder<TransportTask> builder)
        {
            // Héritage TPT : relie à la base
            builder.HasBaseType<TaskItem>();
            builder.ToTable("TransportTask");
            builder.Property(x => x.Support).HasMaxLength(50);

            builder.HasOne(x => x.SourceArea)
                .WithMany()
                .HasForeignKey(x => x.SourceAreaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DestinationArea)
                .WithMany()
                .HasForeignKey(x => x.DestinationAreaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(x => x.SourceArea).AutoInclude();
            builder.Navigation(x => x.DestinationArea).AutoInclude();
        }
    }
}
