using Application.Common.Interfaces;
using System.Security.Claims;

namespace WebApp
{
    /// <summary>
    /// Fournit des informations sur l'utilisateur courant basées sur le contexte HTTP.
    /// Implémente <see cref="IUser"/> et expose l'identifiant et les rôles extraits des claims.
    /// </summary>
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _http;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="CurrentUser"/>.
        /// </summary>
        /// <param name="http">Accesseur au contexte HTTP utilisé pour récupérer l'utilisateur courant.</param>
        public CurrentUser(IHttpContextAccessor http) => _http = http;

        /// <summary>
        /// Obtient l'identifiant de l'utilisateur courant.
        /// </summary>
        /// <remarks>
        /// Cette valeur est l'extraction du claim <see cref="ClaimTypes.NameIdentifier"/>.
        /// Retourne <c>null</c> si aucun contexte HTTP n'est disponible, si l'utilisateur n'est pas authentifié
        /// ou si le claim est absent.
        /// </remarks>
        public string? Id => _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        /// Obtient la liste des rôles associés à l'utilisateur courant.
        /// </summary>
        /// <remarks>
        /// Les rôles sont collectés à partir des claims de type <see cref="ClaimTypes.Role"/>.
        /// Retourne <c>null</c> si aucun contexte HTTP ou utilisateur n'est disponible.
        /// Si l'utilisateur est présent mais n'a pas de claims de rôle, une liste vide est retournée.
        /// </remarks>
        public List<string>? Roles =>
            _http.HttpContext?.User?.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
    }
}
