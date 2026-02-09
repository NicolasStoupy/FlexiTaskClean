using Domain.Entities.MasterData;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //private List<Plant> _plants = new List<Plant>();

        //public IReadOnlyCollection<Plant> Plants => _plants.AsReadOnly();
    }

}
