using Domain.Entities.MasterData;
using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext : IAsyncDisposable
    {
        DbSet<TaskItem> TaskItem { get; }
        DbSet<Plant> Plant { get; }
        DbSet<WorkArea> WorkAreas { get; }
        DbSet<WorkAreaType> WorkAreaTypes { get; }
        DbSet<TaskHeader> TaskHeader { get; }

        DbSet<TransportTask> TransportTasks { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
    public interface IApplicationDbContextFactory
    {
        Task<IApplicationDbContext> CreateAsync(CancellationToken ct = default);
    }

}
