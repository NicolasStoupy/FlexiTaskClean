using Ardalis.GuardClauses;

namespace Application.WorkAreaTypes.Queries.GetWorkAreaType;

public record GetWorkAreaTypeQuery(int workAreaTypeId) : IRequest<WorkAreaTypeDto>
{
}
public class GetWorkAreaTypeQueryHandler : IRequestHandler<GetWorkAreaTypeQuery, WorkAreaTypeDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetWorkAreaTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WorkAreaTypeDto> Handle(GetWorkAreaTypeQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkAreaTypes
            .FindAsync(new object[] { request.workAreaTypeId }, cancellationToken);

        Guard.Against.NotFound(request.workAreaTypeId, entity);

        return _mapper.Map<WorkAreaTypeDto>(entity);
                 
    }
}
