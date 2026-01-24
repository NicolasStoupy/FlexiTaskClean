using Application.Plants.Queries.GetPlants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("plant")]
    public class PlantController : BaseController
    {
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="PlantController"/>.
        /// </summary>
        /// <param name="mediator">
        /// Instance d'<see cref="IMediator"/> fournie par injection de dépendances.
        /// Utilisée pour envoyer des requêtes/commandes (pattern CQRS) vers la couche applicative.
        /// </param>
        /// <remarks>
        /// Le contrôleur délègue l'accès à <see cref="IMediator"/> au <see cref="BaseController"/>.
        /// Assurez-vous que <see cref="IMediator"/> est enregistré dans le conteneur DI.
        /// </remarks>
        public PlantController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var query = new GetPlantsQuery();
            var plants = _mediator.Send(query).Result;
            return Ok(plants);
        }
    }
}
