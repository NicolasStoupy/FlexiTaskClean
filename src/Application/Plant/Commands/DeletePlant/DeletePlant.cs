using Application.Common.Interfaces;
using MediatR;

namespace Application.Plant.Commands.DeletePlant
{
    public record DeletePlantCommand : IRequest<bool>
    {
        public int Id { get; init; }
    }


    public class DeletePlantCommandHandler : IRequestHandler<DeletePlantCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeletePlantCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeletePlantCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.Plant.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            entity.AddDomainEvent(new Domain.Events.PlantDeletedEvent(entity));
            _context.Plant.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }
    }

}
