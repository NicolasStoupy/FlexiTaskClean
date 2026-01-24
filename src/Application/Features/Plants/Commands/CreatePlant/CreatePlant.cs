using Domain.Entities.MasterData;
using Microsoft.EntityFrameworkCore;

namespace Application.Plants.Commands.CreatePlant;

public record CreatePlantCommand(string code, string commonName, string language) : IRequest<int>;

public class CreatePlantCommandValidator : AbstractValidator<CreatePlantCommand>
{
    private readonly IApplicationDbContext _context;
    public CreatePlantCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(x => x.code).NotEmpty().MustAsync(BeUniqueCode).WithMessage("Code already exist").MaximumLength(50);
        RuleFor(x => x.commonName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.language).NotEmpty().MaximumLength(10);
       
    }
    private async Task<bool> BeUniqueCode(string code, CancellationToken ct)
    {
        return !await _context.Plant
            .AsNoTracking()
            .AnyAsync(w => w.Code == code, ct);
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
        var entity = new Plant
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