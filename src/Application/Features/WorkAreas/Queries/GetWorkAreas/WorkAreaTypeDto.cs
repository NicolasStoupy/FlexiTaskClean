using Domain.Entities.MasterData;

namespace Application.Features.WorkAreas.Queries.GetWorkAreas
{
    public class WorkAreaTypeDto
    {

        public string? Code { get; set; }                      
        public string? Label { get; set; }

        public class Mapping : AutoMapper.Profile
        {
            public Mapping()
            {
                CreateMap<WorkAreaType, WorkAreaTypeDto>();
            }
        }
    }
}