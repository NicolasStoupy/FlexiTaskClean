using Domain.Entities.Tasks.TaskSpecializations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.Tasks.TaskSpecializations
{
    public sealed class LoadingTaskLineConfiguration : IEntityTypeConfiguration<LoadingTaskLine>
    {
        public void Configure(EntityTypeBuilder<LoadingTaskLine> builder)
        {
            builder.ToTable("LoadingTaskLine");

            builder.HasKey(x => x.LineItemID);

            builder.Property(x => x.Material).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();

            // index utile
            builder.HasIndex(x => new { x.TaskHeaderID, x.TaskItemID });

            builder.HasOne(x => x.LoadingTask)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => new { x.TaskHeaderID, x.TaskItemID })
                .HasPrincipalKey(x => new { x.TaskHeaderID, x.TaskItemID })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
