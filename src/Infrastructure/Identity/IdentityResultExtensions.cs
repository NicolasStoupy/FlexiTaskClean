using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Infrastructure.Identity
{
    /// <summary>
    /// Contient des méthodes d’extension pour convertir les résultats d’identité ASP.NET
    /// en objets de résultat d’application plus génériques.
    /// </summary>
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// Convertit un <see cref="IdentityResult"/> issu des opérations d’identité ASP.NET Core
        /// (par exemple, création d’utilisateur, changement de mot de passe, ajout de rôle, etc.)
        /// en un objet <see cref="Result"/> propre à la couche Application.
        /// </summary>
        /// <param name="result">
        /// L’objet <see cref="IdentityResult"/> retourné par l’opération d’identité.
        /// </param>
        /// <returns>
        /// Un objet <see cref="Result"/> représentant le succès ou l’échec de l’opération.
        /// </returns>      
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
