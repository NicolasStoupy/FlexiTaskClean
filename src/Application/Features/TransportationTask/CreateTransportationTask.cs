using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TransportationTask
{
    public record CreateTransportationTask(int areaSourceId, int areaDestinationId, string support) : IRequest<Unit>;

    public class CreateTransportationTaskValidator : AbstractValidator<CreateTransportationTask>
    {
        public CreateTransportationTaskValidator()
        {
            RuleFor(x => x.areaSourceId).GreaterThan(0);
            RuleFor(x => x.areaDestinationId).GreaterThan(0);
            RuleFor(x => x.support).NotEmpty().MaximumLength(100);
        }
    }

    public class CreateTransportationTaskHandler : IRequestHandler<CreateTransportationTask, Unit>
    {
        private readonly IApplicationDbContextFactory _factory;

        public CreateTransportationTaskHandler(IApplicationDbContextFactory factory)
        {
            _factory = factory;
        }

        public async Task<Unit> Handle(CreateTransportationTask request, CancellationToken cancellationToken)
            {
            var _context = await _factory.CreateAsync(cancellationToken);
            var taskHeader = new TaskHeader();
            var transportTask = new TransportTask(request.support, request.areaSourceId, request.areaDestinationId);

            taskHeader.AddStartingTask(transportTask);
            _context.TaskHeader.Add(taskHeader);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}