using Application.Common.Interfaces;
using Application.Features.WorkAreas.Queries.GetWorkAreas;

namespace Application.WorkAreas.Queries.GetWorkAreas;

public record GetWorkAreasQuery : IRequest<WorkAreaVm>
{
}

public class GetWorkAreasQueryValidator : AbstractValidator<GetWorkAreasQuery>
{
    public GetWorkAreasQueryValidator()
    {
    }
}

public class GetWorkAreasQueryHandler : IRequestHandler<GetWorkAreasQuery, WorkAreaVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkAreasQueryHandler(IApplicationDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WorkAreaVm> Handle(GetWorkAreasQuery request, CancellationToken cancellationToken)
    {
        return new WorkAreaVm()
        {
            workAreas = await _context.WorkAreas
                .ProjectTo<WorkAreaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}
