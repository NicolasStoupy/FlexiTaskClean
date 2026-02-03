using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Workflows.Queries.GetTaskItem;

public record GetTaskItemQuery : IRequest<bool>
{
    // Idéalement: ajoute un id pour ne pas prendre First()
    // public int TaskHeaderId { get; init; }
}

public class GetTaskItemQueryValidator : AbstractValidator<GetTaskItemQuery>
{
    public GetTaskItemQueryValidator()
    {
    }
}

public class GetTaskItemQueryHandler : IRequestHandler<GetTaskItemQuery, bool>, ICommand
{
    private readonly IApplicationDbContextFactory _dbFactory;
    private readonly IMapper _mapper;

    public GetTaskItemQueryHandler(IApplicationDbContextFactory dbFactory, IMapper mapper)
    {
        _dbFactory = dbFactory;
        _mapper = mapper;
    }

    public async Task<bool> Handle(GetTaskItemQuery request, CancellationToken cancellationToken)
    {
        // ✅ On crée un DbContext "frais"
        await using var db = await _dbFactory.CreateAsync(cancellationToken);

        //var headers = await db.TaskHeader.Where(t => t.Id == 53)
        //    .Include(h => h.TaskItems)
        //        .ThenInclude(t => t.Prerequisites)
        //            .ThenInclude(d => d.DependsOn)
        //    .Include(h => h.TaskItems)
        //        .ThenInclude(t => t.NextSteps)
        //            .ThenInclude(d => d.TaskItem)
        //    .ToListAsync(cancellationToken);
        //foreach (var task in headers.FirstOrDefault().GetNextsRunnableTasks())
        //{
        //    task.Complete();
        //}
        //await db.SaveChangesAsync(cancellationToken);
        var TaskHeaders = TaskHeader.Create();
        await db.SaveChangesAsync(cancellationToken);
        var task = new TransportTask() { 
         SourceAreaId=1,
         DestinationAreaId=2   

        };
        var task1 = TaskHeaders.AddStartingTask(1, task);
        var task2 = TaskHeaders.AddIntermediateTask(1);
        var task3 = TaskHeaders.AddIntermediateTask(2);
        var task4 = TaskHeaders.AddIntermediateTask(3);
        var task5 = TaskHeaders.AddIntermediateTask(9);
        var task6 = TaskHeaders.AddEndingTask(10);
        db.TaskHeader.Add(TaskHeaders);
        await db.SaveChangesAsync(cancellationToken);

        task1.AddNextStep(task2);
        task1.AddNextStep(task3);
        task2.AddNextStep(task4);
        task3.AddNextStep(task5);
        task4.AddNextStep(task6);
        task5.AddNextStep(task6);

        await db.SaveChangesAsync(cancellationToken);

        //// ✅ IMPORTANT: utiliser db (pas _dbFactory)
        //var headers = await db.TaskHeader
        //    .Include(h => h.TaskItems)
        //        .ThenInclude(t => t.Prerequisites)
        //            .ThenInclude(d => d.DependsOn)
        //    .Include(h => h.TaskItems)
        //        .ThenInclude(t => t.NextSteps)
        //            .ThenInclude(d => d.TaskItem)
        //    .ToListAsync(cancellationToken);

        //var header = headers[0];
        //header.AddStartingTask(1);
        //if (header is null)
        //    return false;

        //foreach (var task in header.GetNextsRunnableTasks())
        //{
        //    task.Complete();
        //}

        //// ✅ SaveChanges sur le DbContext créé


        return true;
    }
}
