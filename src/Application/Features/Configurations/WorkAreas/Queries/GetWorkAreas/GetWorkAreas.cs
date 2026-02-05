using Application.Common.Interfaces;
using Application.Common.Security;
using Application.Features.WorkAreas.Queries.GetWorkAreas;

namespace Application.WorkAreas.Queries.GetWorkAreas;


public record GetWorkAreasQuery : IRequest<WorkAreaVm>
{
    public int? PlantID { get; set; }
}

public class GetWorkAreasQueryValidator : AbstractValidator<GetWorkAreasQuery>
{
    public GetWorkAreasQueryValidator()
    {
    }
}

public class GetWorkAreasQueryHandler : IRequestHandler<GetWorkAreasQuery, WorkAreaVm>
{
    IApplicationDbContextFactory _factory;
    private readonly IMapper _mapper;

    public GetWorkAreasQueryHandler(IApplicationDbContextFactory factory, IMapper mapper)
    {
       _factory = factory;
        _mapper = mapper;
    }

    public async Task<WorkAreaVm> Handle(GetWorkAreasQuery request, CancellationToken cancellationToken)
    {
        var _context = await _factory.CreateAsync(cancellationToken);
        if (request.PlantID != null) {
            return new WorkAreaVm()
            {
                workAreas = await _context.WorkAreas.Where(w=>w.PlantID == request.PlantID).AsNoTracking()
                    .ProjectTo<WorkAreaDto>(_mapper.ConfigurationProvider).OrderByDescending(w => w.Active)
                    .ToListAsync(cancellationToken)
            };

        }
        return new WorkAreaVm()
        {
            workAreas = await _context.WorkAreas.AsNoTracking()
                .ProjectTo<WorkAreaDto>(_mapper.ConfigurationProvider).OrderByDescending(w => w.Active)
                .ToListAsync(cancellationToken)
        };
    }
}
