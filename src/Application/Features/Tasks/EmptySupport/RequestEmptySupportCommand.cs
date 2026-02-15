using Ardalis.GuardClauses;
using Domain.Common.Interfaces.Tasks;
using Domain.Factories.Requests;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Features.Tasks.EmptySupport
{
    public record RequestEmptySupportCommand(string supportTypeID, int quantity, string? comment, int destinationAreaID) : IRequest<Unit>,ICommand
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

            var compatibleArea = context.workAreaTransportSupports
                .FirstOrDefault(st => st.SupportTypeID == request.supportTypeID);

            Guard.Against.NotFound(request.supportTypeID, compatibleArea);
            if (compatibleArea == null)
                throw new ApplicationException("Pas de ZOne de transport compatible");
          
            var emptySupportRequest = 
                new EmptySupportTaskRequest(
                    request.supportTypeID,
                    request.quantity,
                    request.comment,
                    request.destinationAreaID,
                    compatibleArea.WorkAreaID
                    );
            var taskHeader = _taskCreationFacade.Create(emptySupportRequest);

            context.TaskHeader.Add(taskHeader);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }


}
