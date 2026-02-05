using Application.Features.WorkAreas.Queries.GetWorkAreas;


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