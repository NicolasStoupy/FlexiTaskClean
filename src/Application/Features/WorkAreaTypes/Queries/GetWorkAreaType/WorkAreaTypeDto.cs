using Domain.Entities.MasterData;

namespace Application.WorkAreaTypes.Queries.GetWorkAreaType
{
    public class WorkAreaTypeDto
    {

        public WorkAreaTypeDto()
        {
            Code = string.Empty;
            Label = string.Empty;
        }

        public int Id { get; set; }
        public string Code { get; init; } 
        public string Label { get; init; }

        public class Mapping : AutoMapper.Profile
        {
            public Mapping()
            {
                CreateMap<WorkAreaType, WorkAreaTypeDto>();
            }
        }
    }
}