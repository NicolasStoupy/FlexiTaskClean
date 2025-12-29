using Domain.Entities;

namespace Application.Plants.Queries.GetPlants
{
    public class WorkAreaDto
    {
        public string Code { get; set; } = null!;

        public string CommonName { get; set; } = null!;

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<WorkArea, WorkAreaDto>();
            }
        }
    }
}