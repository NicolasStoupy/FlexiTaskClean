using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Constants
{
    /// <summary>
    /// Fournit les noms de rôles utilisés pour l'autorisation dans l'application.
    /// </summary>
    /// <remarks>
    /// Cette classe contient des constantes <c>string</c> représentant les rôles (par ex. pour les politiques et l'attribution de claims).
    /// La valeur de chaque constante est définie avec <c>nameof(...)</c> afin de garantir la correspondance entre le nom de la constante
    /// et sa valeur littérale. La classe est déclarée <c>abstract</c> pour empêcher son instanciation.
    /// </remarks>
    public abstract class Roles
    {
        /// <summary>
        /// Rôle d'administrateur disposant des droits les plus élevés (accès et gestion complète).
        /// </summary>
        /// <remarks>
        /// Utiliser cette constante pour attribuer le rôle d'administrateur aux utilisateurs ou pour définir des politiques d'autorisation.
        /// Valeur : "Administrator"
        /// </remarks>
        public const string Administrator = nameof(Administrator);

        /// <summary>
        /// Rôle d'utilisateur standard.
        /// </summary>
        /// <remarks>
        /// Représente les comptes d'utilisateurs normaux sans privilèges administratifs.
        /// Valeur : "Users"
        /// </remarks>
        public const string Users = nameof(Users);
    }
}
