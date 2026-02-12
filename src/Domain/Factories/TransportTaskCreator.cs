using Domain.Common.Exceptions;
using Domain.Common.Interfaces.Tasks;
using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using Domain.Factories.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories
{
    public sealed class TransportTaskCreator : ITaskCreator<CreateOneWayTransportTask>
    {
        public TaskHeader Create(CreateOneWayTransportTask r)
        {
            var header = new TaskHeader();
            var task = new TransportTask(
                r.Support,
                r.DestinationAreaId,
                r.SourceAreaId,
                r.AssignedAreaId,
                r.TargetDate);
            header.AddStartingTask(task);
            return header;
        }
    }

    public sealed class MultiStageTransportTaskCreator : ITaskCreator<CreateMultiStageTransportTask>
    {
        public TaskHeader Create(CreateMultiStageTransportTask request)
        {
            var header = new TaskHeader();
            var areas = request.destinationAreaIDList;

            if (areas == null || areas.Count < 2)
                throw new DomainException("At least two areas are required to create a multi-stage transport task.");
            TaskItem? previousTask = null;
            for (var i = 0; i < areas.Count - 1; i++)
            {
                var fromAreaId = areas[i];
                var toAreaId = areas[i + 1];

                var task = new TransportTask(
                    request.support,
                    toAreaId,
                    fromAreaId,
                    request.assignedAreaID,
                    request.targetDate);

                if (previousTask != null)
                {
                    previousTask.AddNextStep(task);
                }
                previousTask = task;


                if (i == 0)
                    header.AddStartingTask(task);
                else if (i == areas.Count - 2)
                    header.AddEndingTask(task);
                else
                    header.AddIntermediateTask(task);
            }

            return header;
        }

    }

    public sealed class EmptySupportTaskCreator() : ITaskCreator<EmptySupportTaskRequest>
    {
        public TaskHeader Create(EmptySupportTaskRequest request)
        {
            var header = new TaskHeader();
            var supportName = $"Empty {request.supportTypeID}";
            TaskItem previousTask;
            for (var i = 0; i < request.quantity; i++)
            {

                previousTask = new TransportTask(supportName, request.destinationWorkAreaID, request.destinationWorkAreaID, request.destinationWorkAreaID, DateOnly.FromDateTime(DateTime.Now));

                header.AddIntermediateTask(previousTask);

            }

            return header;



        }
    }
}
