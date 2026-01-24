using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// Langues prises en charge pour une usine.
    /// </summary>
    public enum PlantLanguage
    {
        /// <summary>
        /// Langue inconnue ou non spécifiée.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Français.
        /// Code ISO: "fr".
        /// </summary>
        FR,

        /// <summary>
        /// Anglais.
        /// Code ISO: "en".
        /// </summary>
        EN,

        /// <summary>
        /// Allemand (Deutsch).
        /// Code ISO: "de".
        /// </summary>
        DE,

        /// <summary>
        /// Néerlandais (Nederlands).
        /// Code ISO: "nl".
        /// </summary>
        NL,

        /// <summary>
        /// Italien (Italiano).
        /// Code ISO: "it".
        /// </summary>
        IT,

        /// <summary>
        /// Espagnol (Español).
        /// Code ISO: "es".
        /// </summary>
        ES
    }
}
