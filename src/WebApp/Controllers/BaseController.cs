using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Contrôleur de base pour les contrôleurs de l'application.
    /// Fournit un accès centralisé à l'instance <see cref="IMediator"/> de MediatR
    /// afin que les contrôleurs dérivés puissent envoyer des commandes, requêtes et notifications.
    /// </summary>
    public class BaseController : Controller
    {

        /// <summary>
        /// Instance partagée de <see cref="IMediator"/> injectée via le constructeur.
        /// Utilisée par les contrôleurs dérivés pour dispatcher des requêtes/commandes vers les handlers MediatR.
        /// </summary>
        public readonly IMediator _mediator;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="BaseController"/>.
        /// </summary>
        /// <param name="mediator">
        /// L'instance de <see cref="IMediator"/> fournie par l'injection de dépendances.
        /// Ne doit pas être <c>null</c>.
        /// </param>
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
