using Application.Plants.Queries.GetPlants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("plant")]
    public class PlantController : BaseController
    {
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
