using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.Features.Configurations.WorkAreaTypes.Commands.EditWorkAreaType;

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
    private readonly IApplicationDbContextFactory _factory;

    public EditWorkAreaTypeCommandHandler(IApplicationDbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> Handle(EditWorkAreaTypeCommand request, CancellationToken cancellationToken)
    {
        var _context = await _factory.CreateAsync(cancellationToken);
        var entity = await _context.WorkAreaTypes
            .FirstOrDefaultAsync(w => w.WorkAreaTypeID == request.workAreaTypeId, cancellationToken);

        Guard.Against.NotFound(request.workAreaTypeId, entity); 

        entity.Code = request.code;
        entity.Label = request.label;

        await _context.SaveChangesAsync(cancellationToken);
        return entity.WorkAreaTypeID;
    }
}
