using Application.Common.Models;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Fournit des opérations d'identité et d'autorisation abstraites utilisées par l'application.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Récupère le nom d'utilisateur correspondant à l'identifiant d'utilisateur fourni.
        /// </summary>
        /// <param name="userId">L'identifiant de l'utilisateur à rechercher.</param>
        /// <returns>
        /// Le nom d'utilisateur si trouvé ; sinon <c>null</c>.
        /// Opération asynchrone.
        /// </returns>
        Task<string?> GetUserNameAsync(string userId);

        /// <summary>
        /// Détermine si l'utilisateur spécifié appartient à un rôle donné.
        /// </summary>
        /// <param name="userId">L'identifiant de l'utilisateur.</param>
        /// <param name="role">Le nom du rôle à vérifier.</param>
        /// <returns>
        /// <c>true</c> si l'utilisateur est dans le rôle ; sinon <c>false</c>.
        /// Opération asynchrone.
        /// </returns>
        Task<bool> IsInRoleAsync(string userId, string role);

        /// <summary>
        /// Vérifie si l'utilisateur satisfait la politique d'autorisation spécifiée.
        /// </summary>
        /// <param name="userId">L'identifiant de l'utilisateur.</param>
        /// <param name="policyName">Le nom de la politique à vérifier.</param>
        /// <returns>
        /// <c>true</c> si l'utilisateur est autorisé selon la politique ; sinon <c>false</c>.
        /// Opération asynchrone.
        /// </returns>
        Task<bool> AuthorizeAsync(string userId, string policyName);

        /// <summary>
        /// Crée un nouvel utilisateur avec le nom d'utilisateur et le mot de passe fournis.
        /// </summary>
        /// <param name="userName">Le nom d'utilisateur souhaité.</param>
        /// <param name="password">Le mot de passe initial.</param>
        /// <returns>
        /// Un tuple contenant :
        /// - <see cref="Result"/> : le résultat de l'opération (succès/échec et erreurs éventuelles),
        /// - <c>string UserId</c> : l'identifiant de l'utilisateur créé (vide ou null en cas d'échec).
        /// Opération asynchrone.
        /// </returns>
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        /// <summary>
        /// Supprime l'utilisateur identifié par l'identifiant fourni.
        /// </summary>
        /// <param name="userId">L'identifiant de l'utilisateur à supprimer.</param>
        /// <returns>
        /// Un <see cref="Result"/> décrivant le succès ou l'échec de l'opération et les erreurs éventuelles.
        /// Opération asynchrone.
        /// </returns>
        Task<Result> DeleteUserAsync(string userId);
    }
}
