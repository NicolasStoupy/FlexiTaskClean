using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller responsable de la sélection et de la persistance de la culture d'affichage de l'application.
    /// Expose une action permettant de définir la culture courante via un cookie utilisé par
    /// <see cref="CookieRequestCultureProvider"/>.
    /// </summary>
    [Route("culture")]
    public class CultureController : Controller
    {
        /// <summary>
        /// Définit la culture courante et persiste le choix dans un cookie.
        /// </summary>
        /// <param name="culture">
        /// Code de culture demandé (ex. "fr-FR", "en-US"). Si la valeur est nulle ou vide,
        /// l'utilisateur est redirigé vers <paramref name="redirectUri"/> sans modification.
        /// </param>
        /// <param name="redirectUri">
        /// URI relative ou absolue vers laquelle rediriger après définition de la culture.
        /// Valeur par défaut : <c>"/"</c>.
        /// </param>
        /// <returns>
        /// Redirection locale vers <paramref name="redirectUri"/> si la culture est appliquée,
        /// ou redirection simple si le paramètre <paramref name="culture"/> est invalide.
        /// </returns>
        /// <remarks>
        /// - Le cookie utilisé est nommé par <see cref="CookieRequestCultureProvider.DefaultCookieName"/>.
        /// - La valeur du cookie est construite via <see cref="CookieRequestCultureProvider.MakeCookieValue(RequestCulture)"/>.
        /// - Le cookie est marqué comme essentiel (<see cref="CookieOptions.IsEssential"/> = true)
        ///   afin d'être défini même si l'utilisateur n'a pas consenti aux cookies non essentiels.
        /// - La durée d'expiration est fixée à un an depuis la date UTC actuelle.
        /// - Utiliser <see cref="LocalRedirect(string)"/> pour éviter les redirections vers des hôtes externes.
        /// </remarks>
        [HttpGet("SetCulture")]
        public IActionResult Set(string culture, string redirectUri = "/")
        {
            if (string.IsNullOrWhiteSpace(culture))
                return Redirect(redirectUri);

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    Path = "/",
                    IsEssential = true
                });

            return LocalRedirect(redirectUri);
        }
    }
}
