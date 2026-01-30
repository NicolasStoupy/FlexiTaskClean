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
    IApplicationDbContextFactory _factory;

    public DeleteWorkAreaCommandHandler(IApplicationDbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> Handle(DeleteWorkAreaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
