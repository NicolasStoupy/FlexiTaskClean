using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Configurations.Plants.Queries.GetPlants
{
    public class PlantsVm
    {

        public IReadOnlyCollection<PlantDto> PlantLists { get; init; } = Array.Empty<PlantDto>();
    }
}
