    using Application.Common.Interfaces;
    using Domain.Entities.MasterData;
    using Microsoft.EntityFrameworkCore;

    namespace Application.WorkAreas.Commands.CreateWorkArea;

    /// <summary>
    /// Représente la commande pour créer une nouvelle WorkArea.
    /// Retourne l'identifiant (<see cref="int"/>) de l'entité créée.
    /// </summary>
    public record CreateWorkAreaCommand : IRequest<int>, ICommand
    {   
        /// <summary>
        /// Code unique de la zone de travail. Doit être non vide et limité à 5 caractères.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Nom commun/affiché de la zone de travail. Doit être non vide et limité à 50 caractères.
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Identifiant de l'usine (plant) associée à la zone de travail.
        /// Doit être strictement supérieur à 0.
        /// </summary>
        public int PlantID { get; set; }

        /// <summary>
        /// Identifiant du type de zone de travail (work area type).
        /// Doit être strictement supérieur à 0.
        /// </summary>
        public int TypeID { get; set; }
    }

    public class CreateWorkAreaCommandValidator : AbstractValidator<CreateWorkAreaCommand>
    {
        private readonly IApplicationDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance du validateur pour <see cref="CreateWorkAreaCommand"/>.
        /// Définit les règles de validation:
        /// - <see cref="CreateWorkAreaCommand.Code"/> : requis, unique, longueur maximale 5.
        /// - <see cref="CreateWorkAreaCommand.CommonName"/> : requis, longueur maximale 50.
        /// - <see cref="CreateWorkAreaCommand.PlantID"/> : > 0.
        /// - <see cref="CreateWorkAreaCommand.TypeID"/> : > 0.
        /// </summary>
        /// <param name="context">Contexte de la base de données utilisé pour les validations asynchrones (unicité).</param>
        public CreateWorkAreaCommandValidator(IApplicationDbContext context)
        {
            _context = context;
      
            RuleFor(x => x.Code)
                .NotEmpty()
                .MustAsync(BeUniqueCode).WithMessage("Code already exist")
                .MaximumLength(5);

            RuleFor(x => x.CommonName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.PlantID).GreaterThan(0);
            RuleFor(x => x.TypeID).GreaterThan(0);
        }

        /// <summary>
        /// Vérifie de manière asynchrone que le code fourni n'existe pas déjà en base.
        /// </summary>
        /// <param name="code">Code à vérifier.</param>
        /// <param name="ct">Jeton d'annulation.</param>
        /// <returns>
        /// Vrai si aucun WorkArea n'utilise déjà le code; faux sinon.
        /// </returns>
        private async Task<bool> BeUniqueCode(string code, CancellationToken ct)
        {
            return !await _context.WorkAreas
                .AsNoTracking()
                .AnyAsync(w => w.Code == code, ct);
        }
    }

    /// <summary>
    /// Handler qui traite la commande <see cref="CreateWorkAreaCommand"/>.
    /// Crée une nouvelle entité <see cref="WorkArea"/>, l'ajoute au contexte et sauvegarde les changements.
    /// </summary>
    public class CreateWorkAreaCommandHandler : IRequestHandler<CreateWorkAreaCommand, int>
    {
        private readonly IApplicationDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance du handler avec le contexte d'application fourni.
        /// </summary>
        /// <param name="context">Contexte de la base de données.</param>
        public CreateWorkAreaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gère la création de la WorkArea.
        /// - Récupère les entités référencées (<see cref="Domain.Entities.MasterData.Plant"/> et <see cref="Domain.Entities.MasterData.WorkAreaType"/>)
        ///   à partir des identifiants fournis.
        /// - Construit la nouvelle entité <see cref="WorkArea"/>, l'ajoute au contexte et persiste les changements.
        /// </summary>
        /// <param name="request">La commande de création contenant les données nécessaires.</param>
        /// <param name="cancellationToken">Jeton pour l'annulation asynchrone.</param>
        /// <returns>L'identifiant de la WorkArea nouvellement créée.</returns>
        public async Task<int> Handle(CreateWorkAreaCommand request, CancellationToken cancellationToken)
        {
            var plant = await _context.Plant.FindAsync(request.PlantID);
            var type = await _context.WorkAreaTypes.FindAsync(request.TypeID);

            var entity = new WorkArea
            {
                Code = request.Code,
                CommonName = request.CommonName,
                Plant = plant,
                WorkAreaType = type
            };

            _context.WorkAreas.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
