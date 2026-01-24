using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// Pipeline MediatR qui intercepte les exceptions non gérées lors du traitement d'une requête.
    /// Enregistre l'exception et les détails de la requête, puis relance l'exception pour préserver la pile d'appel.
    /// </summary>
    /// <typeparam name="TRequest">Type de la requête (doit être non null).</typeparam>
    /// <typeparam name="TResponse">Type de la réponse renvoyée par le handler.</typeparam>
        public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
    {
        /// <summary>
        /// Logger injecté pour enregistrer les erreurs liées au type de requête.
        /// </summary>
        private readonly ILogger<TRequest> _logger;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="UnhandledExceptionBehaviour{TRequest, TResponse}"/>.
        /// </summary>
        /// <param name="logger">Instance de <see cref="ILogger{TRequest}"/> utilisée pour l'enregistrement des exceptions.</param>
        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Intercepte le traitement de la requête, journalise toute exception non gérée et la relance.
        /// </summary>
        /// <param name="request">La requête en cours de traitement.</param>
        /// <param name="next">Délégué pointant vers l'étape suivante du pipeline (handler ou autre comportement).</param>
        /// <param name="cancellationToken">Token d'annulation pour l'opération asynchrone.</param>
        /// <returns>La réponse produite par l'étape suivante du pipeline.</returns>
        /// <exception cref="System.Exception">Toute exception non gérée est journalisée puis relancée.</exception>
        /// <remarks>
        /// Le comportement enrichit le log avec le nom du type de requête et l'instance de la requête,
        /// afin de faciliter le diagnostic des erreurs en production.
        /// </remarks>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();

            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                var message = ex.Message;
                _logger.LogError(ex, "Flexitask Request: Unhandled Exception for Request {Name} {@Request} {@message}", requestName, request, message);

                throw;
            }
        }
    }

}
