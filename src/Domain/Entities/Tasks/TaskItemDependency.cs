using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tasks
{
    /// <summary>
    /// Représente une dépendance entre deux éléments de tâche.
    /// </summary>
    public class TaskItemDependency : BaseAuditableEntity
    {
        /// <summary>
        /// Identifiant de l'en-tête de tâche auquel appartient l'élément principal.
        /// </summary>
        public int TaskHeaderID { get; set; }

        /// <summary>
        /// Identifiant de l'élément de tâche principal.
        /// </summary>
        public int TaskItemID { get; set; }

        /// <summary>
        /// Identifiant de l'en-tête de tâche dont dépend l'élément principal.
        /// </summary>
        public int DependsOnTaskHeaderID { get; set; }

        /// <summary>
        /// Identifiant de l'élément de tâche dont dépend l'élément principal.
        /// </summary>
        public int DependsOnTaskItemID { get; set; }

        /// <summary>
        /// Navigation vers l'élément de tâche principal.
        /// </summary>
        public TaskItem TaskItem { get; set; } = null!;

        /// <summary>
        /// Navigation vers l'élément de tâche dont dépend l'élément principal.
        /// </summary>
        public TaskItem DependsOn { get; set; } = null!;
    }
}
