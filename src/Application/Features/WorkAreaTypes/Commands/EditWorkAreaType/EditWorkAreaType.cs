using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.WorkAreaTypes.Commands.EditWorkAreaType;

public record EditWorkAreaTypeCommand(int workAreaTypeId, string code, string label) : IRequest<int>
{
}

public class EditWorkAreaTypeCommandValidator : AbstractValidator<EditWorkAreaTypeCommand>
{
    public EditWorkAreaTypeCommandValidator()
    {
    }
}

public class EditWorkAreaTypeCommandHandler : IRequestHandler<EditWorkAreaTypeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public EditWorkAreaTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(EditWorkAreaTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkAreaTypes
            .FirstOrDefaultAsync(w => w.Id == request.workAreaTypeId, cancellationToken);

        Guard.Against.NotFound(request.workAreaTypeId, entity); 

        entity.Code = request.code;
        entity.Label = request.label;

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
