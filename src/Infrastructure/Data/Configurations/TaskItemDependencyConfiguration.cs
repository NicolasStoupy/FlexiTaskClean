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
                x.DependsOnTaskHeaderId,
                x.DependsOnTaskItemId,
                x.TaskHeaderId,
                x.TaskItemId
            });

            builder.Property(x => x.TaskHeaderId)
                .HasColumnName("TaskHeaderID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.TaskItemId)
                .HasColumnName("TaskItemsID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.DependsOnTaskHeaderId)
                .HasColumnName("TaskHeaderID_DependOn")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.DependsOnTaskItemId)
                .HasColumnName("TaskItemsID_DependOn")
                .HasColumnType("int")
                .IsRequired();

            // FK -> TaskItems (la tâche)
            builder.HasOne(x => x.TaskItem)
                .WithMany(t => t.Dependencies) // à ajouter dans TaskItem
                .HasForeignKey(x => new { x.TaskHeaderId, x.TaskItemId })
                .OnDelete(DeleteBehavior.NoAction);

            // FK -> TaskItems (le prérequis)
            builder.HasOne(x => x.DependsOn)
                .WithMany(t => t.DependentBy) // à ajouter dans TaskItem
                .HasForeignKey(x => new { x.DependsOnTaskHeaderId, x.DependsOnTaskItemId })
                .OnDelete(DeleteBehavior.NoAction);

            // Audit (si tu veux mapper explicitement datetimeoffset(7)/varchar(50))
            builder.Property(x => x.Created).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.LastModified).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.LastModifiedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
        }
    }
}
