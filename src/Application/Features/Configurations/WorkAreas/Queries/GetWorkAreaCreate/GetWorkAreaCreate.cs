using Application.Common.Interfaces;
using Application.Features.Configurations.Plants.Queries.GetPlants;
using Application.Features.Configurations.WorkAreaTypes.Queries.GetWorkAreaType;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using System.Threading;

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
    IApplicationDbContextFactory _factory;
    private readonly IMapper _mapper;

    public GetWorkAreaCreateQueryHandler(IApplicationDbContextFactory factory, IMapper mapper)
    {
       _factory = factory;
        _mapper = mapper;
    }

    public async Task<WorkAreaCreateVm> Handle(GetWorkAreaCreateQuery request, CancellationToken ct)
    {
        var _context = await _factory.CreateAsync(ct);
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
