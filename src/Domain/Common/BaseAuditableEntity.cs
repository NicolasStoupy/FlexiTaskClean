namespace Domain.Common
{
    /// <summary>
    /// Classe de base abstraite ajoutant des informations d’audit
    /// (création et dernière modification) aux entités du domaine.
    /// </summary>
    //public abstract class BaseAuditableEntity : BaseEntity
    //{
    //    /// <summary>
    //    /// Date et heure de création de l’entité (en UTC).
    //    /// </summary>
    //    public DateTimeOffset Created { get; set; }

    //    /// <summary>
    //    /// Identifiant de l’utilisateur ayant créé l’entité.
    //    /// </summary>
    //    public string? CreatedBy { get; set; }

    //    /// <summary>
    //    /// Date et heure de la dernière modification de l’entité (en UTC).
    //    /// </summary>
    //    public DateTimeOffset LastModified { get; set; }

    //    /// <summary>
    //    /// Identifiant de l’utilisateur ayant effectué la dernière modification.
    //    /// </summary>
    //    public string? LastModifiedBy { get; set; }

    //}

    public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity
    {
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }

        public DateTimeOffset? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }


    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
