using Application.Common.Interfaces;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Application.WorkAreaTypes.Queries.GetWorkAreaType;

namespace Application.WorkAreaTypes.Queries.GetWorkAreaTypes;

public record GetWorkAreaTypesQuery : IRequest<WorkAreaTypeVm>
{
}

public class GetWorkAreaTypesQueryHandler : IRequestHandler<GetWorkAreaTypesQuery, WorkAreaTypeVm>
{
    IApplicationDbContextFactory _factory;
    private readonly IMapper _mapper;
    public GetWorkAreaTypesQueryHandler(IApplicationDbContextFactory factory,IMapper mapper)
    {
       _factory = factory;
        _mapper = mapper;
        
    }

    public async Task<WorkAreaTypeVm> Handle(GetWorkAreaTypesQuery request, CancellationToken cancellationToken)
    {
        var _context = await _factory.CreateAsync(cancellationToken);
        return new WorkAreaTypeVm() { WorkAreaTypes = await _context.WorkAreaTypes.AsNoTracking()
        .ProjectTo<WorkAreaTypeDto>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken) 
       };
    }
}
