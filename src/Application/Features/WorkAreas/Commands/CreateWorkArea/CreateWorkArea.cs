using Application.Common.Interfaces;
using Domain.Entities.MasterData;

namespace Application.WorkAreas.Commands.CreateWorkArea;

public record CreateWorkAreaCommand : IRequest<int>
{
    public string Code { get; set; }
    public string CommonName { get; set; }
    public int PlantID { get; set; }
    public int TypeID { get; set; }
}

public class CreateWorkAreaCommandValidator : AbstractValidator<CreateWorkAreaCommand>
{
    public CreateWorkAreaCommandValidator()
    {
      
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(5);
        RuleFor(x => x.CommonName)
            .NotEmpty()
            .MaximumLength(50);
    }
}

public class CreateWorkAreaCommandHandler : IRequestHandler<CreateWorkAreaCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateWorkAreaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateWorkAreaCommand request, CancellationToken cancellationToken)
    {
        var plant = await _context.Plant.FindAsync(request.PlantID);
       var type = await _context.WorkAreaTypes.FindAsync(request.TypeID);
        var entity = new WorkArea
        {
            Code = request.Code,
            CommonName = request.CommonName,
            Plant= plant,
            WorkAreaType= type

        };
        _context.WorkAreas.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;

    }
}
