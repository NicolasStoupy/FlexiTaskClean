using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tasks.Queries.TaskList
{
    public record GetTaskListQuery( int areaId, TaskItemStatus taskItemStatus) : IRequest<TaskListVm>;
    
    public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, TaskListVm>
    {
        private readonly IApplicationDbContextFactory _factory;
        private readonly IMapper _mapper;
        public GetTaskListQueryHandler(IApplicationDbContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }
        public async Task<TaskListVm> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
        {
            var _context = await _factory.CreateAsync(cancellationToken);
            var entities = await _context.TransportTasks
                .Where(t => t.LinkedWorkArea == request.areaId && t.TaskItemStatus != TaskItemStatus.Completed)
                .ProjectTo<TaskListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new TaskListVm { Tasks = entities };
        }
    }
}
