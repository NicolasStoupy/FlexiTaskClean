using Application.Common.Models;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TaskExecution
{
    public record TaskHandleTransitionCommand(int taskId) : IRequest<Unit>;

    public class TaskHandleTransitionCommandHandler : IRequestHandler<TaskHandleTransitionCommand, Unit>
    {
        private readonly IApplicationDbContextFactory _factory;
        private readonly IUser _user;
        private readonly IOptions<TaskLockOptions> _options;
        public TaskHandleTransitionCommandHandler(IApplicationDbContextFactory factory, IUser user,IOptions<TaskLockOptions> options)
        {
            _factory = factory;
            _user = user;
            _options = options;
        }
        public async Task<Unit> Handle(TaskHandleTransitionCommand request, CancellationToken ct)
        {
            await using var context = await _factory.CreateAsync(ct);

            var task = await context.TaskItem
                .Where(t => t.TaskItemID == request.taskId)
                .Include(t => t.Prerequisites)
                    .ThenInclude(d => d.DependsOn)
                .Include(t => t.NextSteps)
                    .ThenInclude(d => d.TaskItem)
                .FirstOrDefaultAsync(ct);

            Guard.Against.NotFound(request.taskId, task);
            var lease = TimeSpan.FromMinutes(_options.Value.LeaseMinutes);
            task.Execute(_user.Id ?? string.Empty, lease);

            await context.SaveChangesAsync(ct);
            return Unit.Value;
        }

    }


}
