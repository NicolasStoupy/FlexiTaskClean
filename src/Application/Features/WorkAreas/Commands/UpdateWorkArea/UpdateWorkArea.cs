using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.WorkAreas.Commands.UpdateWorkArea;

public record UpdateWorkAreaCommand : IRequest<int>,ICommand
{
    public int WorkAreaId { get; set; }
    public string Code { get; set; } = "";
    public string CommonName { get; set; } = "";
    public int PlantId { get; set; }
    public int TypeId { get; set; }

    public bool Active { get; set; }
}


public class UpdateWorkAreaCommandValidator : AbstractValidator<UpdateWorkAreaCommand>
{
    public UpdateWorkAreaCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(5);
        RuleFor(x => x.CommonName).NotNull().
          NotEmpty()
          .MaximumLength(50);
        RuleFor(x => x.PlantId).GreaterThan(0);
        RuleFor(x => x.TypeId).GreaterThan(0);
    }
}

public class UpdateWorkAreaCommandHandler : IRequestHandler<UpdateWorkAreaCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateWorkAreaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateWorkAreaCommand request, CancellationToken ct)
    {
        var entity = await _context.WorkAreas
            .FirstOrDefaultAsync(w => w.Id == request.WorkAreaId, ct);
        var plant = await _context.Plant
            .FirstOrDefaultAsync(p => p.Id == request.PlantId, ct);
        var workAreaType = await _context.WorkAreaTypes
            .FirstOrDefaultAsync(t => t.Id == request.TypeId, ct);
        Guard.Against.NotFound(request.PlantId, plant);
        Guard.Against.NotFound(request.TypeId, workAreaType);
        Guard.Against.NotFound(request.WorkAreaId, entity);

        entity.Code = request.Code;
        entity.CommonName = request.CommonName;

        entity.Plant = plant;
        entity.WorkAreaType = workAreaType;
        entity.Active = request.Active;

        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }

}
