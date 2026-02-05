using Application.Features.Configurations.Plants.Queries.GetPlants;
using Application.Features.Configurations.WorkAreaTypes.Queries.GetWorkAreaType;
using Application.Features.WorkAreas.Queries.GetWorkAreas;

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