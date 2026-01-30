using Domain.Entities.MasterData;
using Microsoft.EntityFrameworkCore;

namespace Application.Plants.Commands.CreatePlant;

public record CreatePlantCommand(string code, string commonName, string language) : IRequest<int>,ICommand;

public class CreatePlantCommandValidator : AbstractValidator<CreatePlantCommand>
{
    IApplicationDbContextFactory _factory;
    public CreatePlantCommandValidator(IApplicationDbContextFactory factory)
    {
       _factory = factory;
        RuleFor(x => x.code).NotEmpty().MustAsync(BeUniqueCode).WithMessage("Code already exist").MaximumLength(50);
        RuleFor(x => x.commonName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.language).NotEmpty().MaximumLength(10);
       
    }
    private async Task<bool> BeUniqueCode(string code, CancellationToken ct)
    {
        var _context = await _factory.CreateAsync(ct);
        return !await _context.Plant
            .AsNoTracking()
            .AnyAsync(w => w.Code == code, ct);
    }
}

public class CreatePlantCommandHandler : IRequestHandler<CreatePlantCommand, int>
{
    IApplicationDbContextFactory _factory;

    public CreatePlantCommandHandler(IApplicationDbContextFactory factory)
    {
       _factory = factory;
    }

    public async Task<int> Handle(CreatePlantCommand request, CancellationToken cancellationToken)
    {
        var _context = await _factory.CreateAsync(cancellationToken);
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