using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;

namespace Infrastructure.Data.Configurations.Tasks.TaskSpecializations
{
    public sealed class LoadingTaskConfiguration : IEntityTypeConfiguration<LoadingTask>
    {
        public void Configure(EntityTypeBuilder<LoadingTask> builder)
        {
            // Héritage TPT : relie à la base
            builder.HasBaseType<TaskItem>();

            builder.Property(x => x.Material)
           .HasMaxLength(50)      
           .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Support)
                .HasMaxLength(50);

            builder.HasOne(x => x.AreaForLoading)
                .WithMany()
                .HasForeignKey(x => x.AreaForLoadingID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.AreaForLoadingID);

            builder.Navigation(x => x.AreaForLoading).AutoInclude();
            // Audit (optionnel si tu configures globalement)
            builder.Property(x => x.Created).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.LastModified).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.LastModifiedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
        }
    }
}
