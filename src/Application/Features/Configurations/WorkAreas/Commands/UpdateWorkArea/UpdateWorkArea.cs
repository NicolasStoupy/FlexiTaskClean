using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using System.Threading;

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
    IApplicationDbContextFactory _factory;

    public UpdateWorkAreaCommandHandler(IApplicationDbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> Handle(UpdateWorkAreaCommand request, CancellationToken ct)
    {
        var _context = await _factory.CreateAsync(ct);
        var workArea = await _context.WorkAreas
            .FirstOrDefaultAsync(w => w.WorkAreaID == request.WorkAreaId, ct);

        Guard.Against.NotFound(request.WorkAreaId, workArea);
        workArea.Update(request.Code,request.CommonName, request.PlantId,request.Active);

        await _context.SaveChangesAsync(ct);
        return workArea.WorkAreaID;
    }

}
