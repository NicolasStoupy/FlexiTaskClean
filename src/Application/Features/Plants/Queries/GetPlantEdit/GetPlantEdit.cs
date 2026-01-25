using Application.Common.Interfaces;
using Application.Plants.Queries.GetPlants;

namespace Application.Plants.Queries.GetPlantEdit;

public record GetPlantEditQuery(int plantID) : IRequest<PlantEditVm>;


   



public class GetPlantEditQueryValidator : AbstractValidator<GetPlantEditQuery>
{
    public GetPlantEditQueryValidator()
    {
    }
}

public class GetPlantEditQueryHandler : IRequestHandler<GetPlantEditQuery, PlantEditVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetPlantEditQueryHandler(IApplicationDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlantEditVm> Handle(GetPlantEditQuery request, CancellationToken cancellationToken)
    {
        return new PlantEditVm
        {
            Plant = _mapper.Map<PlantDto>(await _context.Plant
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.plantID, cancellationToken))
        };
    }
}
