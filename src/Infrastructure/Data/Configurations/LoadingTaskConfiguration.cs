using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class LoadingTaskConfiguration : IEntityTypeConfiguration<LoadingTask>
    {
        public void Configure(EntityTypeBuilder<LoadingTask> builder)
        {
            builder.ToTable("LoadingTask");

            // PK = FK composite vers TaskItems
            builder.HasKey(x => new { x.TaskHeaderId, x.TaskItemsId });

            builder.Property(x => x.TaskHeaderId)
                .HasColumnName("TaskHeaderID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.TaskItemsId)
                .HasColumnName("TaskItemsID")
                .HasColumnType("int")
                .IsRequired();

            // ✅ Ici on dit explicitement: LoadingTask est dépendant et porte la FK
            builder.HasOne(x => x.TaskItem)
                .WithOne(ti => ti.LoadingTask)
                .HasForeignKey<LoadingTask>(x => new { x.TaskHeaderId, x.TaskItemsId })
                .OnDelete(DeleteBehavior.NoAction);

            // FK optionnelle vers WorkArea (si ta table a AreaForLoading)
            builder.Property(x => x.AreaForLoading)
                .HasColumnType("int")
                .IsRequired(false);

            builder.HasOne(x => x.LoadingArea)
                .WithMany()
                .HasForeignKey(x => x.AreaForLoading)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
