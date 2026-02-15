using Domain.Common.Interfaces.Tasks;
using Domain.Factories.Requests;

namespace Application.Features.Tasks.TransportationTask
{
    public record CreateTask(
            int AreaSourceId,
            int AreaDestinationId,
            string Support,
            int AreaAssigned,
            DateOnly? TargetDate
        ) : IRequest<Unit>;

    public class CreateTransportationTaskValidator : AbstractValidator<CreateTask>
    {
        public CreateTransportationTaskValidator()
        {
            RuleFor(x => x.AreaSourceId).GreaterThan(0);
            RuleFor(x => x.AreaDestinationId).GreaterThan(0);
            RuleFor(x => x.Support).NotEmpty().MaximumLength(100);
        }
    }

    public class CreateTransportationTaskHandler : IRequestHandler<CreateTask, Unit>
    {
        private readonly IApplicationDbContextFactory _factory;
        private readonly ITaskCreationFacade _taskFacade;

        public CreateTransportationTaskHandler(IApplicationDbContextFactory factory, ITaskCreationFacade taskFacade)
        {
            _factory = factory;
            _taskFacade = taskFacade;
        }

        public async Task<Unit> Handle(CreateTask request, CancellationToken cancellationToken)
        {
            var context = await _factory.CreateAsync(cancellationToken).ConfigureAwait(false);
            var loadingItems = new List<LoadingItems> { };
            loadingItems.Add(new LoadingItems()
            {
                Material = "3050505",
                Description = "FCLL 5.0",
                Quantity = 10,
                SupportTypeID = "X12",
                WorkAreaID = 25,
                AssignedWorkAreaID = 26,
                Support = "PF0520"
            });
            loadingItems.Add(new LoadingItems()
            {
                Material = "3050505",
                Description = "FCLL 5.0",
                Quantity = 2,
                SupportTypeID = "X12",
                WorkAreaID = 27,
                AssignedWorkAreaID = 26,
                Support = "PF0520"
            });
            loadingItems.Add(new LoadingItems()
            {
                Material = "3050505",
                Description = "FCLL 3.0",
                Quantity = 10,
                SupportTypeID = "X12",
                WorkAreaID = 25,
                AssignedWorkAreaID = 26,
                Support = "PF0520"
            });
            var taskLoading = _taskFacade.Create(new CreateLoadingTaskRequests(loadingItems));

            var taskHeader = _taskFacade.Create(
                new CreateOneWayTransportTask(
                    request.Support,
                    request.AreaDestinationId,
                    request.AreaSourceId,
                    request.AreaAssigned,
                    request.TargetDate)
                );

            context.TaskHeader.Add(taskHeader);
            context.TaskHeader.Add(taskLoading);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}