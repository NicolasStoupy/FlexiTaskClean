using Application.Features.Configurations.Plants.Queries.GetPlants;

namespace Application.Features.Configurations.Plants.Queries.GetPlantEdit
{
    public class PlantEditVm
    {

        public PlantEditVm()
        {
           Plant = new PlantDto();
        }
        public PlantDto Plant { get; init; }
    }
}