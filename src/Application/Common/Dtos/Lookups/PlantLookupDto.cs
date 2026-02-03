using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos.Lookups
{
    public record PlantLookupDto(int Id, string Code,string CommonName, string Language,bool Active);
}
