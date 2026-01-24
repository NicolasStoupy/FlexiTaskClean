using Domain.Entities.MasterData;

namespace Application.Features.WorkAreas.Queries.GetWorkAreas
{
    public class WorkAreaDto
    {
        public WorkAreaDto()
        {
            WorkAreaType = new WorkAreaTypeDto();
        }
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string CommonName { get; set; } = null!;
        public int plantID { get; set; }
        public int workAreaTypeID { get; set; }
        public bool Active { get; set; }

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
