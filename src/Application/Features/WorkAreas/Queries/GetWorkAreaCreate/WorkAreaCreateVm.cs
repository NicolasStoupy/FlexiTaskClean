using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Application.Plants.Queries.GetPlants;
using Application.WorkAreaTypes.Queries.GetWorkAreaType;

namespace Application.WorkAreas.Queries.GetWorkAreaCreate
{
    public class WorkAreaCreateVm
    {
        public WorkAreaCreateVm()
        {
            WorkAreaTypes = Array.Empty<WorkAreaTypeDto>();
            Plants = Array.Empty<PlantDto>();
        }
        public IList<WorkAreaTypeDto> WorkAreaTypes { get; init; }

        public IReadOnlyCollection<PlantDto> Plants { get; init; }
    }
}