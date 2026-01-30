namespace Application.Features.Plants.Commands.UpdatePlants
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
                .FirstOrDefaultAsync(p => p.Id == request.PlantID, cancellationToken);
            if (plant != null)
            {
                plant.Language = Enum.Parse<Domain.Enums.PlantLanguage>(request.Language);
                plant.Code = request.Code;
                plant.CommonName = request.CommonName;
                plant.Active = request.Active;

                await _context.SaveChangesAsync(cancellationToken);
                return plant.Id;
            }
            return -1;

        }
    }
}
