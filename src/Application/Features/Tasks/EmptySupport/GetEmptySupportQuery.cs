using Application.Common.Dtos.Lookups;
using Application.Common.Mappings;
using AutoMapper.QueryableExtensions;
using Domain.Common.Interfaces.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tasks.EmptySupport
{
    public record GetEmptySupportQuery(int woareaDestinationID):IRequest<EmptySupportVm>
    {

    }

    public class GetEmptySupportQueryHandler(IApplicationDbContextFactory contextFactory,IMapper mapper,ITaskCreationFacade taskCreationFacade) : IRequestHandler<GetEmptySupportQuery, EmptySupportVm>
    {
        private readonly IApplicationDbContextFactory _contextFactory = contextFactory;
        private readonly IMapper _mapper= mapper;
        private readonly ITaskCreationFacade _taskCreationFacade= taskCreationFacade;   
        public async Task<EmptySupportVm> Handle(GetEmptySupportQuery request, CancellationToken cancellationToken)
        {
            var context = await _contextFactory.CreateAsync(cancellationToken);

            return new EmptySupportVm()
            {
                supportTypes = await context.SupportTypes.ProjectToListAsync<SupportTypeLookupDto>(_mapper.ConfigurationProvider)
            };

        }
    }



}
