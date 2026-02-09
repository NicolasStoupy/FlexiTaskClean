using Domain.Common.Exceptions;
using Domain.Common.Interfaces.Tasks;
using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using Domain.Factories.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public sealed class LoadingTaskCreation : ITaskCreator<CreateLoadingTaskRequests>
    {
        public TaskHeader Create(CreateLoadingTaskRequests request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var items = request.loadingItems;
            if (items is null || items.Count == 0)
                throw new DomainException("At least one loading item is required.");

            var header = new TaskHeader();

            LoadingTask? previousLoadingTask = null;
            var nowDate = DateOnly.FromDateTime(DateTime.UtcNow);
            int count = items.Count;
            bool transportTaskFlag = false;

            for (int i = 0; i < count; i++)
            {
                var item = items[i];

                // 1) Créer la tâche de chargement (étape i)
                var currentLoadingTask = new LoadingTask(
                    item.SupportTypeID,
                    item.Support,
                    item.WorkAreaID);

                // 2) Si on vient d’une autre zone, insérer un transport entre previous -> current
                if (previousLoadingTask is not null &&
                    previousLoadingTask.AreaForLoadingID != currentLoadingTask.AreaForLoadingID)
                {
                    var transportTask = new TransportTask(
                        support: previousLoadingTask.Support,
                        destinationAreaId: currentLoadingTask.AreaForLoadingID,
                        sourceAreaId: previousLoadingTask.AreaForLoadingID,
                        assignedAreaId: item.AssignedWorkAreaID,
                        targetDate: nowDate);

                    header.AddIntermediateTask(transportTask);

                    previousLoadingTask.AddNextStep(transportTask);
                    transportTask.AddNextStep(currentLoadingTask);
                    transportTaskFlag = true;
                }

                // 3) Ajouter la tâche au header (start / intermediate / end)
                if (i == 0)
                    header.AddStartingTask(currentLoadingTask);
                else if (i == count - 1)
                    header.AddEndingTask(currentLoadingTask);
                else
                    header.AddIntermediateTask(currentLoadingTask);

                // 4) Mettre à jour previous pour l'itération suivante
                if (!transportTaskFlag)
                    previousLoadingTask = currentLoadingTask;
            }

            return header;
        }

    }
}