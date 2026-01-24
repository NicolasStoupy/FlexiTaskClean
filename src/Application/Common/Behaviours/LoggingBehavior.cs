using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// Comportement MediatR exécuté avant le traitement d'une requête.
    /// </summary>
    /// <typeparam name="TRequest">Type de la requête MediatR (doit être non-null).</typeparam>
    /// <remarks>
    /// Ce comportement enregistre une entrée de log pour chaque requête reçue, en incluant :
    /// - le nom du type de requête,
    /// - l'identifiant et le nom de l'utilisateur courant via <see cref="IUser"/>,
    /// - l'objet requête sérialisé.
    /// Utilise un <see cref="ILogger{TCategoryName}"/> spécifique au type de requête.
    /// </remarks>
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
     where TRequest : notnull
    {
        /// <summary>
        /// Logger fourni par l'injection de dépendance. Catégorie : <typeparamref name="TRequest"/>.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Fournit les informations de l'utilisateur courant (Id, éventuellement Nom).
        /// </summary>
        private readonly IUser _user;


        /// <summary>
        /// Initialise une nouvelle instance de <see cref="LoggingBehaviour{TRequest}"/>.
        /// </summary>
        /// <param name="logger">Logger typé pour la requête.</param>
        /// <param name="user">Service exposant les informations de l'utilisateur courant.</param>
        public LoggingBehaviour(ILogger<TRequest> logger, IUser user)
        {
            _logger = logger;
            _user = user;
     
        }

        /// <summary>
        /// Méthode appelée avant l'exécution du handler de la requête.
        /// </summary>
        /// <param name="request">Instance de la requête en cours de traitement.</param>
        /// <param name="cancellationToken">Jeton d'annulation.</param>
        /// <remarks>
        /// Récupère le nom du type de requête, l'identifiant utilisateur (vide si absent)
        /// et enregistre ces informations ainsi que la requête via <see cref="_logger"/>.
        /// </remarks>
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _user.Id ?? string.Empty;
            string? userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = _user.Id;
            }

            _logger.LogInformation("FlexiTask Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
