using Application.Common.Interfaces;

namespace Web.Services
{
    public class FakeUser : IUser
    {
        public string? Id => "BE081801";

        public List<string>? Roles => new List<string>();
    }
}
