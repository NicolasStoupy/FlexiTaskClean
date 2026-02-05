using Application.Common.Interfaces;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Ardalis.GuardClauses;
using AutoMapper.QueryableExtensions;
using Domain.Entities.MasterData;
using System.Threading;

namespace Application.WorkAreas.Queries.GetWorkArea;

public record GetWorkAreaQuery(int workAreaID) : IRequest<WorkAreaDto>
{
}

public class GetWorkAreaQueryValidator : AbstractValidator<GetWorkAreaQuery>
{
    public GetWorkAreaQueryValidator()
    {
    }
}

public class GetWorkAreaQueryHandler : IRequestHandler<GetWorkAreaQuery, WorkAreaDto>
{
    IApplicationDbContextFactory _factory;
    private readonly IMapper _mapper;
    public GetWorkAreaQueryHandler(IApplicationDbContextFactory factory, IMapper mapper)
    {
       _factory = factory;
        _mapper = mapper;

    }
    public async Task<WorkAreaDto> Handle(GetWorkAreaQuery request, CancellationToken ct)
    {
        var _context = await _factory.CreateAsync(ct);
        var workArea= await _context.WorkAreas
            .AsNoTracking()
            .Where(w => w.WorkAreaID == request.workAreaID)
            .ProjectTo<WorkAreaDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(ct);
        if(workArea == null)
        {
            throw new NotFoundException(request.workAreaID.ToString(), nameof(WorkArea));
        }
        return workArea;
    }

}
