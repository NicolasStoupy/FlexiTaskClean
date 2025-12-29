namespace Application.Features.Plants.Commands.UpdatePlants
{
    public record UpdatePlantCommand() : IRequest<int>
    {
        public int PlantID { get; set; }
        public string Code { get; init; } = null!;
        public string CommonName { get; init; } = null!;
        public string Language { get; init; } = null!;
    }

    public class UpdatePlantCommandHandler : IRequestHandler<UpdatePlantCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public UpdatePlantCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdatePlantCommand request, CancellationToken cancellationToken)
        {
            var plant = await _context.Plants
                .FirstOrDefaultAsync(p => p.Id == request.PlantID, cancellationToken);
            if (plant != null)
            {
                plant.Language = Enum.Parse<Domain.Enums.PlantLanguage>(request.Language);
                plant.Code = request.Code;
                plant.CommonName = request.CommonName;

                await _context.SaveChangesAsync(cancellationToken);
                return plant.Id;
            }
            return -1;

        }
    }
}
