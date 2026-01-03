using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class TaskItemsConfiguration : IEntityTypeConfiguration<TaskItems>
    {
        public void Configure(EntityTypeBuilder<TaskItems> builder)
        {
            builder.ToTable("TaskItems");

            builder.HasKey(x => new { x.TaskHeaderId, x.TaskItemsId });

            builder.Property(x => x.TaskHeaderId)
                .HasColumnName("TaskHeaderID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.TaskItemsId)
                .HasColumnName("TaskItemsID")
                .HasColumnType("int")
                .ValueGeneratedOnAdd(); // identity(1,1)

            builder.Property(x => x.StartingTask)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.EndingTask)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.TaskDataId)
                .HasColumnName("TaskDataID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.TaskItemTypeId)
                .HasColumnName("TaskItemType_ID")
                .HasColumnType("varchar(4)")
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.TaskStatusId)
                .HasColumnName("TaskStatusID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.LinkedWorkArea)
                .HasColumnType("int")
                .IsRequired(false);

            builder.HasOne(x => x.TaskHeader)
                .WithMany()
                .HasForeignKey(x => x.TaskHeaderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.TaskData)
                .WithMany()
                .HasForeignKey(x => x.TaskDataId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.TaskItemType)
                .WithMany()
                .HasForeignKey(x => x.TaskItemTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.TaskStatus)
                .WithMany()
                .HasForeignKey(x => x.TaskStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.WorkArea)
                .WithMany()
                .HasForeignKey(x => x.LinkedWorkArea)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
