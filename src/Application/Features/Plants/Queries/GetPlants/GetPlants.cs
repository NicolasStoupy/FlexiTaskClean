
using Application.Features.Plants.Queries.GetPlants;

namespace Application.Plants.Queries.GetPlants;

public record GetPlantsQuery (int? plantID = 0) : IRequest<PlantsVm>
{
}

public class GetPlantsQueryValidator : AbstractValidator<GetPlantsQuery>
{
    public GetPlantsQueryValidator()
    {
    }
}

public class GetPlantsQueryHandler : IRequestHandler<GetPlantsQuery, PlantsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPlantsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlantsVm> Handle(GetPlantsQuery request, CancellationToken cancellationToken)
    {
        if(request.plantID != 0 && request.plantID != null)
        {
            var plant = await _context.Plant
            .AsNoTracking()
            .Where(p => p.Id == request.plantID)
            .ProjectTo<PlantDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
            return new PlantsVm()
            {
                PlantLists = plant != null ? new List<PlantDto> { plant } : new List<PlantDto>()
            };
        }
        return new PlantsVm()
        {
            PlantLists = await _context.Plant
            .AsNoTracking()
            .ProjectTo<PlantDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken)
        };
    }
}