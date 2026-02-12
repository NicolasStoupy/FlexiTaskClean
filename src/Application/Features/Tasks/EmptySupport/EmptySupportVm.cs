using Application.Common.Dtos.Lookups;

namespace Application.Features.Tasks.EmptySupport
{
    public class EmptySupportVm
    {

        public IReadOnlyCollection<SupportTypeLookupDto> supportTypes { get; init; } = new List<SupportTypeLookupDto>();

    }
}