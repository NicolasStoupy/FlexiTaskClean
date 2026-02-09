using Domain.Common.Interfaces.Tasks;

namespace Domain.Factories.Requests;

public sealed record CreateOneWayTransportTask(string? Support,
    int DestinationAreaId,
    int SourceAreaId,
    int AssignedAreaId,
    DateOnly? TargetDate) : ITaskCreationRequest
{
    public string TaskKind => "Transport";
}


public sealed record CreateMultiStageTransportTask(string? support, List<int> destinationAreaIDList, int assignedAreaID,DateOnly? targetDate) : ITaskCreationRequest
{
    public string TaskKind => "Transport";
}

public sealed record CreateLoadingTask(
    int WorkAreaId,
    string StillageType, 
    decimal AmountSpace,
    IReadOnlyList<LoadingLine> Lines) : ITaskCreationRequest
{
    public string TaskKind => "Loading";
}
public sealed record LoadingLine(int MaterialId, int Qty);