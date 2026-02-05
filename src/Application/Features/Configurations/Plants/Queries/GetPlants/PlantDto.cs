using AutoMapper;
using Domain.Entities.MasterData;


namespace Application.Features.Configurations.Plants.Queries.GetPlants
{
    public class PlantDto
    {
        public PlantDto()
        {
            WorkAreas = Array.Empty<PlantWorkAreaDto>();
        }
        public int PlantID { get; set; }
        public string Code { get; init; } = null!;
        public string CommonName { get; init; } =null!;
        public string Language { get; init; }= null!;
        public bool Active { get; set; }
        public  IReadOnlyCollection<PlantWorkAreaDto> WorkAreas { get; init; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Plant, PlantDto>();
            }
        }
    }
}