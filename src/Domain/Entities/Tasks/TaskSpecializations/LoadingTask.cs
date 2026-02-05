using Domain.Common.Exceptions;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks.TaskSpecializations;

public class LoadingTask : TaskItem
{
    private LoadingTask() { } // EF

    public LoadingTask(string material, double qty, int areaForLoadingId, string? support = null)
    {
        if (string.IsNullOrWhiteSpace(material)) throw new DomainException("Material is required");
        if (qty <= 0) throw new DomainException("Qty must be > 0");
        if (areaForLoadingId <= 0) throw new DomainException("AreaForLoadingID invalid");

        Material = material.Trim();
        Quantity = qty;
        AreaForLoadingID = areaForLoadingId;
        Support = support;
    }

    public string Material { get; private set; } = null!;
    public double Quantity { get; private set; }
    public string? Support { get; private set; }

    public int AreaForLoadingID { get; private set; }
    public WorkArea AreaForLoading { get; private set; } = null!;
}
