using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos.Lookups
{
    public record WorkAreaTypeLookupDto(
        int Id,
        string Code,
        string Label
    );
}
