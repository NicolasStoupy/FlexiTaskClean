using Application.Features.WorkAreas.Queries.GetWorkAreas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos.WorkAreas
{
    public class WorkAreaTransportDto:WorkAreaDto
    {
        public string TruckName { get; init; }
        public double MaxLoad { get; init; }
    }
}
