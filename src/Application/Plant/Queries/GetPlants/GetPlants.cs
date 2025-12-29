using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Plant.Queries.GetPlants
{
    public record GetPlantsQuery : IRequest<PlantsVm>;


    public class GetPlantsQueryHandler : IRequestHandler<GetPlantsQuery, PlantsVm>
    {

        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetPlantsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PlantsVm> Handle(GetPlantsQuery request, CancellationToken cancellationToken)
        {
            return new PlantsVm
            {
                Lists = await _context.Plant
                .AsNoTracking()
                .ProjectTo<PlantDTO>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Code)
                .ToListAsync(cancellationToken)
            };
        }
    }

}
