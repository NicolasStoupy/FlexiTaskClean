using Application.Common.Dtos.WorkAreas;
using Application.Features.WorkAreas.Queries.GetWorkAreas;

namespace Application.Features.WorkAreas
{
    public class WorkAreaMainVm
    {

        public IReadOnlyCollection<WorkAreaTaskStatsDto> WorkAreas { get; init; }
    }
}