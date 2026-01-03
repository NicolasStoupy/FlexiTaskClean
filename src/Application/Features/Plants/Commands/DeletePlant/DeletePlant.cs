using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Events;

namespace Application.Plants.Commands.DeletePlant;

public record DeletePlantCommand(int plantID) : IRequest;

public class DeletePlantCommandValidator : AbstractValidator<DeletePlantCommand>
{
    public DeletePlantCommandValidator()
    {
    }
}

public class DeletePlantCommandHandler : IRequestHandler<DeletePlantCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePlantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePlantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Plant
            .FindAsync(new object[] { request.plantID }, cancellationToken);

        Guard.Against.NotFound(request.plantID, entity);

        _context.Plant.Remove(entity);

        entity.AddDomainEvent(new PlantDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
