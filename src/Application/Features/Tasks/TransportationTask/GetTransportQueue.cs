using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tasks.TransportationTask
{
    public record GetTransportQueueQuery(int areaID) : IRequest<List<TransportQueue>>;

    public class GetTransportQueueHandler : IRequestHandler<GetTransportQueueQuery, List<TransportQueue>>
    {

        private readonly IApplicationDbContextFactory _factory;
        private readonly IMapper _mapper;
        public GetTransportQueueHandler(IApplicationDbContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<List<TransportQueue>> Handle(GetTransportQueueQuery request, CancellationToken cancellationToken)
        {
            var context = await _factory.CreateAsync(cancellationToken);
          
            // Historique limité : 3 dernières Completed
            var lastCompleted = await context.TransportTasks
                .Where(t => t.LinkedWorkArea == request.areaID
                         && t.TaskItemStatus == TaskItemStatus.Completed)
                .OrderByDescending(t => t.LastModified)   // ou CompletedAt si tu as
                .Take(3)
                .AsNoTracking()
                .ProjectTo<TransportQueue>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            // Actives : Ready + InProgress
            var activeTasks = await context.TransportTasks
                .Where(t => t.LinkedWorkArea == request.areaID
                         && (t.TaskItemStatus == TaskItemStatus.Ready
                          || t.TaskItemStatus == TaskItemStatus.InProgress))
                .AsNoTracking()
                .ProjectTo<TransportQueue>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            // Merge
            var result = activeTasks
                .Concat(lastCompleted)
                .ToList().OrderBy(a=>a.TaskID).ToList();

            return result;
        }
    }
}
