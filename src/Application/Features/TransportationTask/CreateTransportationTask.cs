using Domain.Common.Interfaces.Tasks;
using Domain.Factories.Requests;

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
            RuleFor(x => x.Support).NotEmpty().MaximumLength(100);
        }
    }

    public class CreateTransportationTaskHandler : IRequestHandler<CreateTransportationTask, Unit>
    {
        private readonly IApplicationDbContextFactory _factory;
        private readonly ITaskCreationFacade _taskFacade;

        public CreateTransportationTaskHandler(IApplicationDbContextFactory factory, ITaskCreationFacade taskFacade)
        {
            _factory = factory;
            _taskFacade = taskFacade;
        }

        public async Task<Unit> Handle(CreateTransportationTask request, CancellationToken cancellationToken)
        {
            var context = await _factory.CreateAsync(cancellationToken).ConfigureAwait(false);

            var taskHeaders = _taskFacade.Create(
                new CreateMultiStageTransportTask(
                    request.Support,
                    new List<int>() { 25, 27, 25, 27 },
                    request.AreaAssigned,
                    request.TargetDate));

            var taskHeader = _taskFacade.Create(
                new CreateOneWayTransportTask(
                    request.Support,
                    request.AreaDestinationId,
                    request.AreaSourceId,
                    request.AreaAssigned,
                    request.TargetDate)
                );

            context.TaskHeader.Add(taskHeaders);

            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}