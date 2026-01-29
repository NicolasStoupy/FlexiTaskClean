using Application.Common.Interfaces;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Application.WorkAreaTypes.Queries.GetWorkAreaType;

namespace Application.WorkAreaTypes.Queries.GetWorkAreaTypes;

public record GetWorkAreaTypesQuery : IRequest<WorkAreaTypeVm>
{
}

public class GetWorkAreaTypesQueryHandler : IRequestHandler<GetWorkAreaTypesQuery, WorkAreaTypeVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetWorkAreaTypesQueryHandler(IApplicationDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }

    public async Task<WorkAreaTypeVm> Handle(GetWorkAreaTypesQuery request, CancellationToken cancellationToken)
    {
       return new WorkAreaTypeVm() { WorkAreaTypes = await _context.WorkAreaTypes.AsNoTracking()
        .ProjectTo<WorkAreaTypeDto>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken) 
       };
    }
}
