using Application.Features.Tasks.Queries.TaskList;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Domain.Entities.MasterData;
using Domain.Entities.Tasks.TaskSpecializations;

namespace Application.Features.Tasks.TransportationTask.Queries.GetTransportation
{
    public class CreateTransportationVm
    {

        public List<WorkAreaDto> WorkAreas { get; set; } = new();

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<WorkArea, WorkAreaDto>();
                  

            }
        }
    }
}