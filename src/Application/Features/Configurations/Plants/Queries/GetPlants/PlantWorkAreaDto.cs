using Domain.Entities.MasterData;

namespace Application.Features.Configurations.Plants.Queries.GetPlants
{
    public class PlantWorkAreaDto
    {
        public string Code { get; set; } = null!;

        public string CommonName { get; set; } = null!;

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<WorkArea, PlantWorkAreaDto>();
            }
        }
    }
}