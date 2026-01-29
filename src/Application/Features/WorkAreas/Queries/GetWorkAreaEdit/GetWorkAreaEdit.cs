using Application.Features.WorkAreas.Queries.GetWorkAreaEdit;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Application.Plants.Queries.GetPlants;
using Application.WorkAreaTypes.Queries.GetWorkAreaType;
using Ardalis.GuardClauses;

public record GetWorkAreaEditQuery(int WorkAreaId) : IRequest<WorkAreaEditVm>;

public class GetWorkAreaEditQueryValidator : AbstractValidator<GetWorkAreaEditQuery>
{
    public GetWorkAreaEditQueryValidator()
        => RuleFor(v => v.WorkAreaId).GreaterThan(0);
}

public class GetWorkAreaEditQueryHandler : IRequestHandler<GetWorkAreaEditQuery, WorkAreaEditVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkAreaEditQueryHandler(IApplicationDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);

    public async Task<WorkAreaEditVm> Handle(GetWorkAreaEditQuery request, CancellationToken ct)
    {
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
