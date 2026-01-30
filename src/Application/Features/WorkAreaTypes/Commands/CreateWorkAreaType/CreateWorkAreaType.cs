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
    private readonly IApplicationDbContextFactory _factory;

    public CreateWorkAreaTypeCommandHandler(IApplicationDbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> Handle(CreateWorkAreaTypeCommand request, CancellationToken cancellationToken)
    {
        var context = await _factory.CreateAsync();

        var workAreaType = new WorkAreaType()
        {
            Code = request.Code,
            Label = request.label
        };
        context.WorkAreaTypes.Add(workAreaType);
        await context.SaveChangesAsync(cancellationToken);
        return workAreaType.Id;
    }
}