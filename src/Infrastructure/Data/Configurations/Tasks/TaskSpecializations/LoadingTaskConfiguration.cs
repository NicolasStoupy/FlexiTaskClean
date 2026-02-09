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

            builder.ToTable("LoadingTask");
            builder.Property(x => x.Support)
                .HasMaxLength(50);
            builder.Property(x => x.AreaForLoadingID).IsRequired();
            builder.HasOne(x => x.AreaForLoading)
                .WithMany()
                .HasForeignKey(x => x.AreaForLoadingID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SupportType)
              .WithMany()
              .HasForeignKey(x => x.SupportTypeID)
              .OnDelete(DeleteBehavior.Restrict);
            builder.Navigation(x => x.Lines).HasField("_lines").UsePropertyAccessMode(PropertyAccessMode.Field); ;

            builder.HasMany(x => x.Lines)
              .WithOne(l => l.LoadingTask)
              .HasForeignKey(l => new { l.TaskHeaderID, l.TaskItemID })
              .HasPrincipalKey(p => new { p.TaskHeaderID, p.TaskItemID })
              .OnDelete(DeleteBehavior.Cascade);
   
            builder.HasIndex(x => x.AreaForLoadingID);

            builder.Navigation(x => x.AreaForLoading).AutoInclude();
            builder.Navigation(x => x.Lines).AutoInclude();
            // Audit (optionnel si tu configures globalement)
            builder.Property(x => x.Created).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.LastModified).HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.LastModifiedBy).HasColumnType("varchar(50)").HasMaxLength(50).IsUnicode(false);
        }
    }
}