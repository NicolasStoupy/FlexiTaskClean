using Application.Common.Dtos.WorkAreas;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Domain.Entities.Tasks.TaskSpecializations;

namespace Application.Features.WorkAreas
{
    public record GetWorkAreaMain(int plantID) : IRequest<WorkAreaMainVm>;

    public class GetWorkAreaQueryHandler(IApplicationDbContextFactory factory, IMapper mapper) : IRequestHandler<GetWorkAreaMain, WorkAreaMainVm>
    {

        private readonly IApplicationDbContextFactory _factory = factory;
        private readonly IMapper _mapper = mapper;

        public async Task<WorkAreaMainVm> Handle(GetWorkAreaMain request, CancellationToken ct)
        {
            await using var context = await _factory.CreateAsync(ct);

            var activeStatuses = new[] { TaskItemStatus.Ready, TaskItemStatus.InProgress };

            // 1) Zones (on récupère brut)
            var areas = await context.WorkAreas
                .AsNoTracking()
                .Where(w => w.PlantID == request.plantID)
                .Select(w => new { w.WorkAreaID, w.Code, w.CommonName })
                .ToListAsync(ct);

            var areaIds = areas.Select(a => a.WorkAreaID).ToList();

            // 2) Chargement
            var loadingByArea = await context.TaskItem
                .OfType<LoadingTask>()
                .AsNoTracking()
                .Where(t => areaIds.Contains(t.LinkedWorkArea) && activeStatuses.Contains(t.TaskItemStatus))
                .GroupBy(t => t.LinkedWorkArea)
                .Select(g => new { AreaId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.AreaId, x => x.Count, ct);

            // 3) Déchargement
            var unloadingByArea = await context.TaskItem
                .OfType<TransportTask>()
                .AsNoTracking()
                .Where(t => areaIds.Contains(t.LinkedWorkArea) && activeStatuses.Contains(t.TaskItemStatus))
                .GroupBy(t => t.LinkedWorkArea)
                .Select(g => new { AreaId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.AreaId, x => x.Count, ct);

            // 4) Transport entrant InProgress
            var transportIncomingByArea = await context.TaskItem
                .OfType<TransportTask>()
                .AsNoTracking()
                .Where(t => areaIds.Contains(t.DestinationAreaId) && t.TaskItemStatus == TaskItemStatus.InProgress)
                .GroupBy(t => t.DestinationAreaId)
                .Select(g => new { AreaId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.AreaId, x => x.Count, ct);

            // 5) Projection finale (init-only)
            var result = areas.Select(a => new WorkAreaTaskStatsDto
            {
                WorkAreaID = a.WorkAreaID,
                Code = a.Code,
                CommonName = a.CommonName,
                LoadingReadyOrInProgress =
                    loadingByArea.TryGetValue(a.WorkAreaID, out var lc) ? lc : 0,
                UnloadingReadyOrInProgress =
                    unloadingByArea.TryGetValue(a.WorkAreaID, out var uc) ? uc : 0,
                TransportInProgressIncoming =
                    transportIncomingByArea.TryGetValue(a.WorkAreaID, out var tc) ? tc : 0
            }).ToList();

            return new WorkAreaMainVm { WorkAreas = result };
        }



    }
}
