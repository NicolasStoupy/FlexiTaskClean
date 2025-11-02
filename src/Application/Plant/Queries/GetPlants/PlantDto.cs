using AutoMapper;

namespace Application.Plant.Queries.GetPlants
{
    public class PlantDTO
    {

        public PlantDTO()
        {

        }

        public int Id { get; set; }
        public string Code { get; set; }

        public bool IsActive { get; set; }


        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Plant, PlantDTO>();
            }
        }
    }
}
