using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TaskExecution
{
    public record TaskHandleTransitionCommand(int taskId) : IRequest<Unit>;

    public class TaskHandleTransitionCommandHandler : IRequestHandler<TaskHandleTransitionCommand, Unit>
    {
        private readonly IApplicationDbContextFactory _factory;
        public TaskHandleTransitionCommandHandler(IApplicationDbContextFactory factory)
        {
            _factory = factory;
        }
        public async Task<Unit> Handle(TaskHandleTransitionCommand request, CancellationToken cancellationToken)
        {
            var _context = await _factory.CreateAsync(cancellationToken);
            var taskItemA = _context.TaskItem.Where(f=>f.TaskItemID== request.taskId).FirstOrDefault(); 
            var headers = await _context.TaskHeader.Where(t => t.TaskHeaderID == taskItemA.TaskHeaderID)
           .Include(h => h.TaskItems)
               .ThenInclude(t => t.Prerequisites)
                   .ThenInclude(d => d.DependsOn)
           .Include(h => h.TaskItems)
               .ThenInclude(t => t.NextSteps)
                   .ThenInclude(d => d.TaskItem)
           .ToListAsync(cancellationToken);
            var task= headers.FirstOrDefault().TaskItems.Where(t => t.TaskItemID == request.taskId).FirstOrDefault();
  
            Guard.Against.NotFound(request.taskId, task);

            task.Execute();

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }


}
