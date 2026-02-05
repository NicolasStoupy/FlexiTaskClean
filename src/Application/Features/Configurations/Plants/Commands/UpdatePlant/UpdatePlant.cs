namespace Application.Features.Configurations.Plants.Commands.UpdatePlant
{
    public record UpdatePlantCommand() : IRequest<int>,ICommand
    {
        public int PlantID { get; set; }
        public string Code { get; set; } = null!;
        public string CommonName { get; set; } = null!;
        public string Language { get; set; } = null!;
        public bool Active { get; set; }
    }

    public class UpdatePlantCommandHandler : IRequestHandler<UpdatePlantCommand, int>
    {
        IApplicationDbContextFactory _factory;
        public UpdatePlantCommandHandler(IApplicationDbContextFactory factory)
        {
           _factory = factory;
        }
        public async Task<int> Handle(UpdatePlantCommand request, CancellationToken cancellationToken)
        {
            var _context = await _factory.CreateAsync(cancellationToken);
            var plant = await _context.Plant
                .FirstOrDefaultAsync(p => p.PlantID == request.PlantID, cancellationToken);
            if (plant != null)
            {
                plant.Update(request.Code, request.CommonName, request.Language,request.Active);
                await _context.SaveChangesAsync(cancellationToken);
                return plant.PlantID;
            }
            return -1;

        }
    }
}
