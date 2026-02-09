using Domain.Common.Exceptions;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks.TaskSpecializations;

public class LoadingTask : TaskItem
{
    private LoadingTask() { } // EF    

    public string SupportTypeID { get; private set; }
    public string? Support { get; private set; }
    public int AreaForLoadingID { get; private set; }

    public SupportType SupportType { get; private set; } = null!;

    private readonly List<LoadingTaskLine> _lines = new();
    public IReadOnlyCollection<LoadingTaskLine> Lines => _lines;
    public WorkArea AreaForLoading { get; private set; } = null!;
}
