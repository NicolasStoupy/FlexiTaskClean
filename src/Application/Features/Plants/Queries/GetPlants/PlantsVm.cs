using Application.Plants.Queries.GetPlants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Plants.Queries.GetPlants
{
    public class PlantsVm
    {

        public IReadOnlyCollection<PlantDto> PlantLists { get; init; } = Array.Empty<PlantDto>();
    }
}
