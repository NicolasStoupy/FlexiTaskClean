using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.WorkAreaTypes.Commands.DeleteWorkAreaType;

public record DeleteWorkAreaTypeCommand(int workAreaTypeID) : IRequest<bool>
{
}

public class DeleteWorkAreaTypeCommandValidator : AbstractValidator<DeleteWorkAreaTypeCommand>
{
    public DeleteWorkAreaTypeCommandValidator()
    {
    }
}

public class DeleteWorkAreaTypeCommandHandler : IRequestHandler<DeleteWorkAreaTypeCommand, bool>
{
    private readonly IApplicationDbContextFactory _factory;

    public DeleteWorkAreaTypeCommandHandler(IApplicationDbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<bool> Handle(DeleteWorkAreaTypeCommand request, CancellationToken cancellationToken)
    {
        var _context = await _factory.CreateAsync(cancellationToken);
        var entity = await _context.WorkAreaTypes
            .FindAsync(new object[] { request.workAreaTypeID }, cancellationToken);
        Guard.Against.NotFound(request.workAreaTypeID, entity);
        _context.WorkAreaTypes.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;

    }
}
