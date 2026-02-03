using Domain.Entities.Tasks.TaskSpecializations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public sealed class TransportTaskConfiguration : IEntityTypeConfiguration<TransportTask>
    {
        public void Configure(EntityTypeBuilder<TransportTask> builder)
        {
            // C'est cette ligne qui active le TPT et dit à EF 
            // "les propriétés ci-dessous sont dans cette table spécifique"
            builder.ToTable("TransportTask");
            builder.Property(x => x.TaskHeaderId).HasColumnName("TaskHeaderID");

       
        }
    }
}
