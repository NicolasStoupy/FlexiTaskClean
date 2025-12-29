using Application.Common.Interfaces;
using MediatR;

namespace Application.Plant.Commands.UpdatePlant
{
    public record UpdatePlantCommand:IRequest<int>
    {

        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

    }

    public class UpdatePlantHandler : IRequestHandler<UpdatePlantCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public UpdatePlantHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdatePlantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Plant.FindAsync(new object[] { request.Id }, cancellationToken);
                if (entity == null)
                {
                    throw new Exception("Plant not found");
                }
                entity.Code = request.Code;
                if (request.IsActive)
                    entity.Activate();
                else
                    entity.Deactivate();
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
    }
}
