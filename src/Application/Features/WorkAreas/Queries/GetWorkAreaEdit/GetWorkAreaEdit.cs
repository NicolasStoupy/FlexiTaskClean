using Application.Features.WorkAreas.Queries.GetWorkAreaEdit;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Application.Plants.Queries.GetPlants;
using Application.WorkAreaTypes.Queries.GetWorkAreaType;
using Ardalis.GuardClauses;
using System.Threading;

public record GetWorkAreaEditQuery(int WorkAreaId) : IRequest<WorkAreaEditVm>;

public class GetWorkAreaEditQueryValidator : AbstractValidator<GetWorkAreaEditQuery>
{
    public GetWorkAreaEditQueryValidator()
        => RuleFor(v => v.WorkAreaId).GreaterThan(0);
}

public class GetWorkAreaEditQueryHandler : IRequestHandler<GetWorkAreaEditQuery, WorkAreaEditVm>
{
    IApplicationDbContextFactory _factory;
    private readonly IMapper _mapper;

    public GetWorkAreaEditQueryHandler(IApplicationDbContextFactory factory, IMapper mapper)
        => (_factory, _mapper) = (factory, mapper);

    public async Task<WorkAreaEditVm> Handle(GetWorkAreaEditQuery request, CancellationToken ct)
    {
        var _context = await _factory.CreateAsync(ct);
        var workArea = await _context.WorkAreas
            .AsNoTracking()
            .Where(w => w.Id == request.WorkAreaId)
            .ProjectTo<WorkAreaDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);

        Guard.Against.NotFound(request.WorkAreaId, workArea);

        var workAreaTypes = await _context.WorkAreaTypes
            .AsNoTracking()
            .OrderBy(t => t.Code)
            .ProjectTo<WorkAreaTypeDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);

        var Plants = await _context.Plant
            .AsNoTracking()
            .ProjectTo<PlantDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);

        return new WorkAreaEditVm
        {
            WorkArea = workArea!,
            WorkAreaTypes = workAreaTypes,
            plants= Plants
        };
    }
}
