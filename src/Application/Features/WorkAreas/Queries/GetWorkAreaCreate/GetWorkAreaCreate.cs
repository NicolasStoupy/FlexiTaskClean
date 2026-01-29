using Application.Common.Interfaces;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Application.Plants.Queries.GetPlants;
using Application.WorkAreaTypes.Queries.GetWorkAreaType;

namespace Application.WorkAreas.Queries.GetWorkAreaCreate;

public record GetWorkAreaCreateQuery : IRequest<WorkAreaCreateVm>
{
}

public class GetWorkAreaCreateQueryValidator : AbstractValidator<GetWorkAreaCreateQuery>
{
    public GetWorkAreaCreateQueryValidator()
    {
    }
}

public class GetWorkAreaCreateQueryHandler : IRequestHandler<GetWorkAreaCreateQuery, WorkAreaCreateVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkAreaCreateQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WorkAreaCreateVm> Handle(GetWorkAreaCreateQuery request, CancellationToken ct)
    {
        var workAreaTypes = await _context.WorkAreaTypes
          .AsNoTracking()
          .OrderBy(t => t.Code)
          .ProjectTo<WorkAreaTypeDto>(_mapper.ConfigurationProvider)
          .ToListAsync(ct);

        var Plants = await _context.Plant
            .AsNoTracking()
            .ProjectTo<PlantDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);

        return new WorkAreaCreateVm()
        {
            WorkAreaTypes = workAreaTypes,
            Plants = Plants
        };
    }
}
