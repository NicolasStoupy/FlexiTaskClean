namespace Application.Plant.Queries.GetPlants
{
    public class PlantsVm
    {
        public IReadOnlyCollection<PlantDTO> Lists { get; init; } = Array.Empty<PlantDTO>();
    }
}

