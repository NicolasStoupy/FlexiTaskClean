using Domain.Entities.MasterData;

namespace Application.WorkAreaTypes.Commands.CreateWorkAreaType;

public record CreateWorkAreaTypeCommand(string Code, string label) : IRequest<int>
{
}

public class CreateWorkAreaTypeCommandValidator : AbstractValidator<CreateWorkAreaTypeCommand>
{
    public CreateWorkAreaTypeCommandValidator()
    {
    }
}

public class CreateWorkAreaTypeCommandHandler : IRequestHandler<CreateWorkAreaTypeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateWorkAreaTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateWorkAreaTypeCommand request, CancellationToken cancellationToken)
    {

        var workAreaType = new WorkAreaType()
        {
            Code = request.Code,
            Label = request.label
        };        
        _context.WorkAreaTypes.Add(workAreaType);
        await _context.SaveChangesAsync(cancellationToken);
        return workAreaType.Id;
    }
}