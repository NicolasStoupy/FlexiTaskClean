using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    /// <summary>
    /// Interface indiquant qu'une entité est auditable.
    /// </summary>
    /// <remarks>
    /// Fournit des propriétés pour tracer la date/heure de création et de dernière modification,
    /// ainsi que l'identifiant (ou nom) de l'utilisateur ayant effectué ces opérations.
    /// L'utilisation de <see cref="DateTimeOffset"/> permet de préserver le décalage temporel.
    /// </remarks>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Date et heure de création de l'entité.
        /// </summary>
        /// <remarks>
        /// Doit être initialisée lors de la création de l'entité.
        /// </remarks>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Identifiant ou nom de l'utilisateur qui a créé l'entité.
        /// </summary>
        /// <remarks>
        /// Peut être <c>null</c> si l'information n'est pas disponible (par ex. action système).
        /// </remarks>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Date et heure de la dernière modification effectuée sur l'entité.
        /// </summary>
        /// <remarks>
        /// <c>null</c> si l'entité n'a jamais été modifiée après sa création.
        /// </remarks>
        public DateTimeOffset? LastModified { get; set; }

        /// <summary>
        /// Identifiant ou nom de l'utilisateur qui a effectué la dernière modification.
        /// </summary>
        /// <remarks>
        /// Peut être <c>null</c> si l'information n'est pas disponible.
        /// </remarks>
        public string? LastModifiedBy { get; set; }
    }   
}
