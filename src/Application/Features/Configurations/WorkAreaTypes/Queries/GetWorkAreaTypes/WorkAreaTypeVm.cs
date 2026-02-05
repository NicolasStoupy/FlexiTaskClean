using Application.Features.Configurations.WorkAreaTypes.Queries.GetWorkAreaType;
using Application.Features.WorkAreas.Queries.GetWorkAreas;

namespace Application.Features.Configurations.WorkAreaTypes.Queries.GetWorkAreaTypes
{
    public class WorkAreaTypeVm
    {
        public WorkAreaTypeVm() {

            WorkAreaTypes = Array.Empty<WorkAreaTypeDto>();

        }

        public IReadOnlyCollection<WorkAreaTypeDto> WorkAreaTypes { get; init; }
    }
}