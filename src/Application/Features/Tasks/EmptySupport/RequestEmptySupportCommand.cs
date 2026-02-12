using Domain.Common.Interfaces.Tasks;
using Domain.Factories.Requests;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Features.Tasks.EmptySupport
{
    public record RequestEmptySupportCommand(string supportTypeID, int quantity, string? comment, int destinationAreaID) : IRequest<Unit>
    {
    }

    public class RequestEmptySupportCommandHandler(IApplicationDbContextFactory dbContextFactory, ITaskCreationFacade taskCreationFacade, IMapper mapper) : IRequestHandler<RequestEmptySupportCommand, Unit>
    {
        private readonly IApplicationDbContextFactory _dbContextFactory = dbContextFactory;
        private readonly IMapper _mapper = mapper;
        private readonly ITaskCreationFacade _taskCreationFacade = taskCreationFacade;
        public async Task<Unit> Handle(RequestEmptySupportCommand request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateAsync(cancellationToken);
            var emptySupportRequest = new EmptySupportTaskRequest(request.supportTypeID, request.quantity, request.comment, request.destinationAreaID);
            var taskHeader = _taskCreationFacade.Create(emptySupportRequest);

            context.TaskHeader.Add(taskHeader);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }


}
