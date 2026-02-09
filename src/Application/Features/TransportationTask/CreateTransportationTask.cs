using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;

namespace Application.Features.TransportationTask
{
    public record CreateTransportationTask(
            int AreaSourceId,
            int AreaDestinationId,
            string Support,
            int AreaAssigned,
            DateOnly? TargetDate
        ) : IRequest<Unit>;

    public class CreateTransportationTaskValidator : AbstractValidator<CreateTransportationTask>
    {
        public CreateTransportationTaskValidator()
        {
            RuleFor(x => x.AreaSourceId).GreaterThan(0);
            RuleFor(x => x.AreaDestinationId).GreaterThan(0);
            //RuleFor(x => x.Support).NotEmpty().MaximumLength(100);
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
            var context = await _factory.CreateAsync(cancellationToken).ConfigureAwait(false);

            foreach (var item in request.Support.Split(';'))
            {
                var taskHeader = new TaskHeader();
                var transportTask = new TransportTask(
                    item,
                    request.AreaDestinationId,
                    request.AreaSourceId,
                    request.AreaAssigned,
                    request.TargetDate
                );

                taskHeader.AddStartingTask(transportTask);
                context.TaskHeader.Add(taskHeader);

            }
          

            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}