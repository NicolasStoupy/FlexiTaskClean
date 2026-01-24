using Application.Common.Interfaces;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Ardalis.GuardClauses;
using AutoMapper.QueryableExtensions;
using Domain.Entities.MasterData;

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
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetWorkAreaQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }
    public async Task<WorkAreaDto> Handle(GetWorkAreaQuery request, CancellationToken ct)
    {
        var workArea= await _context.WorkAreas
            .AsNoTracking()
            .Where(w => w.Id == request.workAreaID)
            .ProjectTo<WorkAreaDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(ct);
        if(workArea == null)
        {
            throw new NotFoundException(request.workAreaID.ToString(), nameof(WorkArea));
        }
        return workArea;
    }

}
