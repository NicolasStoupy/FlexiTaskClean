using Application.Common.Interfaces;

namespace Application.WorkAreas.Commands.UpdateWorkArea;

public record UpdateWorkAreaCommand : IRequest<int>
{
}

public class UpdateWorkAreaCommandValidator : AbstractValidator<UpdateWorkAreaCommand>
{
    public UpdateWorkAreaCommandValidator()
    {
    }
}

public class UpdateWorkAreaCommandHandler : IRequestHandler<UpdateWorkAreaCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateWorkAreaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateWorkAreaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
