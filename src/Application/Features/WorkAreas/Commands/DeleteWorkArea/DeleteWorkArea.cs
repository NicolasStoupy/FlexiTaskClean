using Application.Common.Interfaces;

namespace Application.WorkAreas.Commands.DeleteWorkArea;

public record DeleteWorkAreaCommand : IRequest<int>
{
}

public class DeleteWorkAreaCommandValidator : AbstractValidator<DeleteWorkAreaCommand>
{
    public DeleteWorkAreaCommandValidator()
    {
    }
}

public class DeleteWorkAreaCommandHandler : IRequestHandler<DeleteWorkAreaCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteWorkAreaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteWorkAreaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
