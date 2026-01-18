using Domain.Entities.Tasks.TaskSpecializations;
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

            // ✅ PK composite (shared PK)
            builder.HasKey(x => new { x.TaskHeaderId, x.TaskItemId });

            builder.Property(x => x.TaskHeaderId)
                .HasColumnName("TaskHeaderID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.TaskItemId)
                .HasColumnName("TaskItemsID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Product)
                .HasColumnName("Product")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Qty)
                .HasColumnName("Qty")
                .HasColumnType("float")
                .IsRequired();

            builder.Property(x => x.Support)
                .HasColumnName("Support")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.AreaForLoadingId)
                .HasColumnName("AreaForLoading")
                .HasColumnType("int");

            // ✅ 1–1 avec TaskItem via PK partagé (composite)
            builder.HasOne(x => x.TaskItem)
                .WithOne(t => t.LoadingTask) // si tu ajoutes la navigation
                .HasForeignKey<LoadingTask>(x => new { x.TaskHeaderId, x.TaskItemId })
                .OnDelete(DeleteBehavior.NoAction);

            // FK WorkArea
            builder.HasOne(x => x.AreaForLoading)
                .WithMany()
                .HasForeignKey(x => x.AreaForLoadingId)
                .OnDelete(DeleteBehavior.NoAction);

            // Audit (optionnel si tu configures globalement)
            builder.Property(x => x.Created).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.LastModified).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.LastModifiedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
        }
    }
}
