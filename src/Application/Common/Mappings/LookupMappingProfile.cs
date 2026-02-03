using Application.Common.Dtos.Lookups;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Mappings
{
    public class LookupMappingProfile : Profile
    {
        public LookupMappingProfile()
        {
            CreateMap<WorkArea, WorkAreaLookupDto>();
            CreateMap<Plant, PlantLookupDto>();
            CreateMap<WorkAreaType, WorkAreaTypeLookupDto>();
        }
    }
}
