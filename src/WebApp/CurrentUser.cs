using Application.Common.Interfaces;
using System.Security.Claims;

namespace WebApp
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUser(IHttpContextAccessor http) => _http = http;

        public string? Id => _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public List<string>? Roles =>
            _http.HttpContext?.User?.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
    }
}
