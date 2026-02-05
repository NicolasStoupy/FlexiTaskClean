using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Events;

namespace Application.Features.Configurations.Plants.Commands.DeletePlant;

public record DeletePlantCommand(int plantID) : IRequest<Unit>;

public class DeletePlantCommandValidator : AbstractValidator<DeletePlantCommand>
{
    public DeletePlantCommandValidator()
    {
    }
}

public class DeletePlantCommandHandler : IRequestHandler<DeletePlantCommand,Unit>
{
    IApplicationDbContextFactory _factory;

    public DeletePlantCommandHandler(IApplicationDbContextFactory factory)
    {
       _factory = factory;
    }

    public async Task<Unit> Handle(DeletePlantCommand request, CancellationToken cancellationToken)
    {
        var _context = await _factory.CreateAsync(cancellationToken);
        var entity = await _context.Plant
            .FindAsync(new object[] { request.plantID }, cancellationToken);

        Guard.Against.NotFound(request.plantID, entity);

        _context.Plant.Remove(entity);

        entity.AddDomainEvent(new PlantDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
