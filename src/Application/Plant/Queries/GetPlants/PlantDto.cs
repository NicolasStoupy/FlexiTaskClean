using AutoMapper;

namespace Application.Plant.Queries.GetPlants
{
    public class PlantDTO
    {

        public PlantDTO()
        {
            WorkAreas = Array.Empty<WorkAreaDTO>();
        }

        public int id { get; set; }
        public string Code { get; set; }

        public bool IsActive { get; set; }

        public IReadOnlyCollection<WorkAreaDTO> WorkAreas { get; init; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Plant, PlantDTO>();
            }
        }
    }
}
