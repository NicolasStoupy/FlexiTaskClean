using Application.Features.Configurations.Plants.Queries.GetPlants;
using Application.Features.Configurations.WorkAreaTypes.Queries.GetWorkAreaType;
using Application.Features.WorkAreas.Queries.GetWorkAreas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.WorkAreas.Queries.GetWorkAreaEdit
{
    public class WorkAreaEditVm
    {
        public WorkAreaEditVm()
        {
            WorkArea = new WorkAreaDto();
            WorkAreaTypes =  Array.Empty<WorkAreaTypeDto>();
            plants = Array.Empty<PlantDto>();
        }
        public WorkAreaDto WorkArea { get; init; }
        public IList<WorkAreaTypeDto> WorkAreaTypes { get; init; }

        public IReadOnlyCollection<PlantDto> plants { get; init; }
    }
}
