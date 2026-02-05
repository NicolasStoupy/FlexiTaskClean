using Ardalis.GuardClauses;

namespace Application.Features.Configurations.WorkAreaTypes.Queries.GetWorkAreaType;

public record GetWorkAreaTypeQuery(int workAreaTypeId) : IRequest<WorkAreaTypeDto>
{
}
public class GetWorkAreaTypeQueryHandler : IRequestHandler<GetWorkAreaTypeQuery, WorkAreaTypeDto>
{
    private readonly IApplicationDbContextFactory _factory;
    private readonly IMapper _mapper;
    public GetWorkAreaTypeQueryHandler(IApplicationDbContextFactory factory, IMapper mapper)
    {
        _factory = factory;
        _mapper = mapper;
    }

    public async Task<WorkAreaTypeDto> Handle(GetWorkAreaTypeQuery request, CancellationToken cancellationToken)
    {
        var context = await _factory.CreateAsync(cancellationToken);
        var entity = await context.WorkAreaTypes
            .FindAsync(new object[] { request.workAreaTypeId }, cancellationToken);

        Guard.Against.NotFound(request.workAreaTypeId, entity);

        return _mapper.Map<WorkAreaTypeDto>(entity);
                 
    }
}
