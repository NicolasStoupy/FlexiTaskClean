using Application.Features.WorkAreas.Queries.GetWorkAreas;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos.WorkAreas
{
    public class WorkAreaTaskStatsDto: WorkAreaDto
    {

        public int LoadingReadyOrInProgress { get; init; }
        public int UnloadingReadyOrInProgress { get; init; }
        public int TransportInProgressIncoming { get; init; }
    }
}
