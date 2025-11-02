using Application.Common.Interfaces;
using MediatR;

namespace Application.Plant.Commands.CreatePlant;

public record CreatePlantCommand : IRequest<int>
{
    public string? Code { get; set; }


}

public class CreatePlantCommandHandler : IRequestHandler<CreatePlantCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreatePlantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreatePlantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new Domain.Entities.Plant();

            entity.Code = request.Code;


            _context.Plant.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            entity.AddDomainEvent(new Domain.Events.PlantCreatedEvent(entity));
            return entity.Id;
        }
        catch (Exception ex)
        {

            throw;
        }

    }
}
