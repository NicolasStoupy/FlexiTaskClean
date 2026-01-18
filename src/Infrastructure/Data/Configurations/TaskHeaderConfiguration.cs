using Domain.Entities.Tasks;

namespace Infrastructure.Data.Configurations
{
    public sealed class TaskHeaderConfiguration : IEntityTypeConfiguration<TaskHeader>
    {
        public void Configure(EntityTypeBuilder<TaskHeader> builder)
        {
            builder.ToTable("TaskHeader");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("TaskHeaderID")
                .ValueGeneratedOnAdd(); // IDENTITY
        }
    }
}
