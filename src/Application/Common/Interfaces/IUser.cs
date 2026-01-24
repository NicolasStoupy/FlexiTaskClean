using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Représente l'utilisateur courant dans le contexte de l'application.
    /// </summary>
    /// <remarks>
    /// Les informations peuvent être absentes (null) si l'utilisateur n'est pas authentifié
    /// ou si le contexte ne fournit pas ces données.
    /// </remarks>
    public interface IUser
    {
        /// <summary>
        /// Identifiant unique de l'utilisateur (par exemple un GUID ou un identifiant externe).
        /// </summary>
        /// <value>Chaîne représentant l'identifiant ou <c>null</c> si non authentifié.</value>
        string? Id { get; }

        /// <summary>
        /// Liste des rôles attribués à l'utilisateur.
        /// </summary>
        /// <remarks>
        /// Utilisé par les mécanismes d'autorisation pour déterminer les droits.
        /// Peut être <c>null</c> ou vide si aucun rôle n'est assigné.
        /// </remarks>
        List<string>? Roles { get; }

    }

}
