using Application.Plants.Queries.GetPlants;

namespace Application.Plants.Queries.GetPlantEdit
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