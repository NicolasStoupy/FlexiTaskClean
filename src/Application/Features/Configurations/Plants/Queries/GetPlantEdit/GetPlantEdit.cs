using Application.Common.Interfaces;
using Application.Features.Configurations.Plants.Queries.GetPlants;

namespace Application.Features.Configurations.Plants.Queries.GetPlantEdit;

public record GetPlantEditQuery(int plantID) : IRequest<PlantEditVm>;

public class GetPlantEditQueryValidator : AbstractValidator<GetPlantEditQuery>
{
    public GetPlantEditQueryValidator()
    {
    }
}

public class GetPlantEditQueryHandler : IRequestHandler<GetPlantEditQuery, PlantEditVm>
{
    private IApplicationDbContextFactory _factory;
    private readonly IMapper _mapper;

    public GetPlantEditQueryHandler(IApplicationDbContextFactory factory, IMapper mapper)
    {
        _factory = factory;
        _mapper = mapper;
    }

    public async Task<PlantEditVm> Handle(GetPlantEditQuery request, CancellationToken cancellationToken)
    {
        var _context = await _factory.CreateAsync(cancellationToken);
        return new PlantEditVm
        {
            Plant = _mapper.Map<PlantDto>(await _context.Plant
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.PlantID == request.plantID, cancellationToken))
        };
    }
}