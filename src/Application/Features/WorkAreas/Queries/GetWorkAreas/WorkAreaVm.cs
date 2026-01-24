using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Application.Plants.Queries.GetPlants;

namespace Application.WorkAreas.Queries.GetWorkAreas
{
    public class WorkAreaVm
    {
        public WorkAreaVm()
        {
            workAreas = Array.Empty<WorkAreaDto>();
        
        }
        public IReadOnlyCollection<WorkAreaDto> workAreas { get; init; }
      



    }
}