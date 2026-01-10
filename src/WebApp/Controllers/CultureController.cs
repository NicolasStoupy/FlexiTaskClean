using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("culture")]
    public class CultureController : Controller
    {
        [HttpGet("SetCulture")]
        public IActionResult Set(string culture, string redirect = "/")
        {
            if (string.IsNullOrWhiteSpace(culture))
                return Redirect(redirect);

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    Path = "/",
                    IsEssential = true
                });

            return LocalRedirect(redirect);
        }
    }
}
