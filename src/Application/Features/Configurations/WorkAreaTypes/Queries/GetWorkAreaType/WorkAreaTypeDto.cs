using Domain.Entities.MasterData;

namespace Application.Features.Configurations.WorkAreaTypes.Queries.GetWorkAreaType
{
    public class WorkAreaTypeDto
    {

        public WorkAreaTypeDto()
        {
            Code = string.Empty;
            Label = string.Empty;
        }

        public int WorkAreaTypeID { get; set; }
        public string Code { get; init; } 
        public string Label { get; init; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<WorkAreaType, WorkAreaTypeDto>();
            }
        }
    }
}