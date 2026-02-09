using Ardalis.GuardClauses;
using Domain.Entities.MasterData;
using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.GetTaskList
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
          
            var entities = await _context.TaskItem.
                Where(Ti => Ti.LinkedWorkArea == request.areaId 
                && Ti.TaskItemStatus== TaskItemStatus.Ready
                || Ti.TaskItemStatus == TaskItemStatus.InProgress)
                .ToListAsync(cancellationToken);
               


            var dtos = _mapper.Map<List<TaskListItemDto>>(entities);
            return new TaskListVm { Tasks = dtos };
        }
    }
}
