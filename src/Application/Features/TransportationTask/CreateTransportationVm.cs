
using Application.Common.Dtos.Lookups;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Domain.Entities.MasterData;
using Domain.Entities.Tasks.TaskSpecializations;

namespace Application.Features.TransportationTask
{
    public class CreateTransportationVm
    {

       

        public List<SupportTypeLookupDto> supportTypeLookups { get; init; } = new();

        public List<WorkAreaLookupDto> workAreaLookups { get; init; } = new();

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<WorkArea, WorkAreaDto>();
                CreateMap<SupportType, SupportTypeLookupDto>();
                CreateMap<WorkArea, WorkAreaLookupDto>();

            }
        }
    }
}