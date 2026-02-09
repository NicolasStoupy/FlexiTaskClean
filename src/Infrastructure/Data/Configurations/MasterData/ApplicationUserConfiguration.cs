using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations.MasterData
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //builder.Navigation(e => e.Plants).HasField("_plants");
               
        }
    }
}
