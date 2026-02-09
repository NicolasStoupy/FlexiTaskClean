using Application.Common.Dtos.Lookups;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Domain.Entities.MasterData;

namespace Application.Features.TransportationTask
{
    public record GetCreateTransportationTask(int workAreaId) : IRequest<CreateTransportationVm>;

    public class GetCreateTransportationTaskHandler : IRequestHandler<GetCreateTransportationTask, CreateTransportationVm>
    {
        private readonly IApplicationDbContextFactory _factory;
        private readonly IMapper _mapper;

        public GetCreateTransportationTaskHandler(IApplicationDbContextFactory factory, IMapper mapper)
        {
            _mapper = mapper;
            _factory = factory;
        }

        public async Task<CreateTransportationVm> Handle(GetCreateTransportationTask request, CancellationToken cancellationToken)
        {
            var dbContext = await _factory.CreateAsync();

            var areas = await dbContext.WorkAreas
                .Where(w => !(w is WorkAreaTransport))   // exclut les WorkAreaTransport
                .AsNoTracking()
                .ProjectTo<WorkAreaLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            var supportLookup = await dbContext.SupportTypes
                .ProjectTo<SupportTypeLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CreateTransportationVm
            {
                workAreaLookups = areas,
                supportTypeLookups = supportLookup
            };
        }
    }

    public record GetTransportationAreaForSupport(string supportTypeID) : IRequest<List<WorkAreaLookupDto>>;
    public class GetTransportationAreaForSupportTaskHandler(IMapper mapper, IApplicationDbContextFactory factory) : IRequestHandler<GetTransportationAreaForSupport, List<WorkAreaLookupDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IApplicationDbContextFactory _factory = factory;

        public async Task<List<WorkAreaLookupDto>> Handle(GetTransportationAreaForSupport request, CancellationToken cancellationToken)
        {
            var context = await _factory.CreateAsync();

            return await context.WorkAreas
                .OfType<WorkAreaTransport>()
                .Where(w => w.SupportedTypes.Any(st => st.SupportTypeID == request.supportTypeID))
                .AsNoTracking()
                .ProjectTo<WorkAreaLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}