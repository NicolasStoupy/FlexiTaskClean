using Domain.Entities.DynamicForms;

namespace Infrastructure.Data.Configurations
{
    public sealed class TaskDataConfiguration : IEntityTypeConfiguration<TaskData>
    {
        public void Configure(EntityTypeBuilder<TaskData> builder)
        {
            builder.ToTable("TaskData", t =>
            {
                t.HasCheckConstraint("CK_JsonTaskData_ValidJson", "ISJSON([JsonData]) = 1");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("TaskDataID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.JsonData)
                .HasColumnName("JsonData")
                .HasColumnType("varchar(max)")   // ou nvarchar(max) recommandé
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.ExternalLink)
                .HasColumnName("ExternalLink")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsUnicode(false);

        }

    }
}
