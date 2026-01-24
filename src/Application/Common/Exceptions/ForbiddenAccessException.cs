using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    /// <summary>
    /// Exception levée lorsque l'utilisateur courant n'a pas la permission d'exécuter l'action demandée.
    /// </summary>
    /// <remarks>
    /// Représente généralement une situation correspondant à HTTP 403 Forbidden au niveau de l'application.
    /// Cette exception n'embarque pas de données supplémentaires par défaut ; si un contexte additionnel
    /// (par exemple la ressource concernée ou la permission requise) est nécessaire, des constructeurs
    /// ou propriétés supplémentaires peuvent être ajoutés.
    /// </remarks>
    public class ForbiddenAccessException : Exception
    {
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="ForbiddenAccessException"/>.
        /// </summary>
        public ForbiddenAccessException() : base() { }
    }
}
