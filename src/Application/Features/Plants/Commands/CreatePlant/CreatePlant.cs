namespace Application.Plants.Commands.CreatePlant;

public record CreatePlantCommand(string code, string commonName, string language) : IRequest<int>;

public class CreatePlantCommandValidator : AbstractValidator<CreatePlantCommand>
{
    public CreatePlantCommandValidator()
    {
        RuleFor(x => x.code).NotEmpty().MaximumLength(50);
        RuleFor(x => x.commonName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.language).NotEmpty().MaximumLength(10);
    }
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
        var entity = new Domain.Entities.Plant
        {
            Code = request.code,
            CommonName = request.commonName,
            Language = Enum.Parse<PlantLanguage>(request.language)
        };
        _context.Plant.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}