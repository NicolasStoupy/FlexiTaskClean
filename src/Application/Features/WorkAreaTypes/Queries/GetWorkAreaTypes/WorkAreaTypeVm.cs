using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Application.WorkAreaTypes.Queries.GetWorkAreaType;

namespace Application.WorkAreaTypes.Queries.GetWorkAreaTypes
{
    public class WorkAreaTypeVm
    {
        public WorkAreaTypeVm() {

            WorkAreaTypes = Array.Empty<WorkAreaTypeDto>();

        }

        public IReadOnlyCollection<WorkAreaTypeDto> WorkAreaTypes { get; init; }
    }
}