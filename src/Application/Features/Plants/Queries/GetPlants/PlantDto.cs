using AutoMapper;
using Domain.Entities;


namespace Application.Plants.Queries.GetPlants
{
    public class PlantDto
    {
        public PlantDto()
        {
            WorkAreas = Array.Empty<WorkAreaDto>();
        }
        public int Id { get; set; }
        public string Code { get; init; } = null!;
        public string CommonName { get; init; } =null!;
        public string Language { get; init; }= null!;   

        public  IReadOnlyCollection<WorkAreaDto> WorkAreas { get; init; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Plant, PlantDto>();
            }
        }
    }
}