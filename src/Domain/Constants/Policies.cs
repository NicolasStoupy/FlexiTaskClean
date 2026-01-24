using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Constants
{
    /// <summary>
    /// Contient les constantes des noms de politiques d'autorisation utilisées dans l'application.
    /// </summary>
    /// <remarks>
    /// Centralise les noms de politiques pour éviter les littéraux dispersés dans le code.
    /// Exemple d'utilisation : <c>[Authorize(Policy = Policies.CanPurge)]</c>.
    /// </remarks>
    public abstract class Policies
    {
        /// <summary>
        /// Politique donnant le droit de purger (suppression permanente) des données.
        /// </summary>
        /// <remarks>
        /// Définir la logique de cette politique lors de la configuration des services d'autorisation,
        /// par exemple dans <c>services.AddAuthorization</c> ou via un <c>AuthorizationPolicyBuilder</c>.
        /// </remarks>
        public const string CanPurge = nameof(CanPurge);
    }
}
