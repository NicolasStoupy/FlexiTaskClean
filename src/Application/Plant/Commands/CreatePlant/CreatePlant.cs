using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Plant.Commands.CreatePlant;

public record CreatePlantCommand : IRequest<Result<int>>
{
    public string? Code { get; set; }
}

public class CreatePlantCommandHandler : IRequestHandler<CreatePlantCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public CreatePlantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreatePlantCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Plant();

        entity.Code = request.Code;

        _context.Plant.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new Domain.Events.PlantCreatedEvent(entity));
        return Result<int>.Success(entity.Id);
    }
}