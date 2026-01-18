using Domain.Entities.MasterData;

namespace Application.Features.WorkAreas.Queries.GetWorkAreas
{
    public class WorkAreaDto
    {
        public WorkAreaDto()
        {
            WorkAreaType= new WorkAreaTypeDto();
        }
        public string Code { get; set; } = null!;               
        public string CommonName { get; set; } = null!;         

        public WorkAreaTypeDto WorkAreaType { get; init; } 

        public class Mapping : AutoMapper.Profile
        {
            public Mapping()
            {
                CreateMap<WorkArea, WorkAreaDto>();
            }
        }
    }
}
