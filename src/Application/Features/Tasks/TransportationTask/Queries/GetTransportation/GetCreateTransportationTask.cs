using Application.Features.WorkAreas.Queries.GetWorkAreas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tasks.TransportationTask.Queries.GetTransportation
{
    public record GetCreateTransportationTask(int workAreaId):IRequest<CreateTransportationVm>;

    public class GetCreateTransportationTaskHandler : IRequestHandler<GetCreateTransportationTask, CreateTransportationVm>
    {
        private readonly IApplicationDbContextFactory _factory;
        private readonly IMapper _mapper;
        public GetCreateTransportationTaskHandler(IApplicationDbContextFactory factory,IMapper mapper)
        {
            _mapper = mapper;
            _factory = factory;
        }
        public async Task<CreateTransportationVm> Handle(GetCreateTransportationTask request, CancellationToken cancellationToken)
        {
            var dbContext = await _factory.CreateAsync();
            
            var areas = await dbContext.WorkAreas                
                .AsNoTracking()
                .ProjectTo<WorkAreaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CreateTransportationVm
            {
                WorkAreas = areas
            };
        }
    }
}
