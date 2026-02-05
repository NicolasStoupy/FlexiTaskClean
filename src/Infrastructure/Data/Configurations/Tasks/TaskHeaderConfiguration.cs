using Domain.Entities.Tasks;

namespace Infrastructure.Data.Configurations.Tasks
{
    public sealed class TaskHeaderConfiguration : IEntityTypeConfiguration<TaskHeader>
    {
        public void Configure(EntityTypeBuilder<TaskHeader> builder)
        {
            builder.ToTable("TaskHeader");

            builder.HasKey(x => x.TaskHeaderID);
            builder.Property(x => x.TaskHeaderID)
                .HasColumnName("TaskHeaderID")
                .ValueGeneratedOnAdd();
            builder.HasMany(th=>th.TaskItems)
                .WithOne()
                .HasForeignKey(ti=>ti.TaskHeaderID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(th => th.TaskItems).HasField("_taskItems");
        }
    }
}
