
using Application.Common.Security;
using Application.Features.Plants.Queries.GetPlants;


namespace Application.Plants.Queries.GetPlants;

[Authorize(Roles = $"{Roles.Administrator},{Roles.Users}")]
public record GetPlantsQuery (int? plantID = 0) : IRequest<PlantsVm>;


public class GetPlantsQueryValidator : AbstractValidator<GetPlantsQuery>
{
    public GetPlantsQueryValidator()
    {
    }
}

public class GetPlantsQueryHandler : IRequestHandler<GetPlantsQuery, PlantsVm>
{
    IApplicationDbContextFactory _factory;
    private readonly IMapper _mapper;

    public GetPlantsQueryHandler(IApplicationDbContextFactory factory, IMapper mapper)
    {
       _factory = factory;
        _mapper = mapper;
    }

    public async Task<PlantsVm> Handle(GetPlantsQuery request, CancellationToken cancellationToken)
    {
        var _context = await _factory.CreateAsync(cancellationToken);
        if (request.plantID != 0 && request.plantID != null)
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
            .ProjectTo<PlantDto>(_mapper.ConfigurationProvider).OrderByDescending(w => w.Active)
            .ToListAsync(cancellationToken)
        };
    }
}