using Domain.Common.Interfaces.Tasks;
using Domain.Entities.Tasks;
using Domain.Factories.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories
{
    public sealed class LoadingTaskCreation : ITaskCreator<CreateLoadingTaskRequests>
    {
        public TaskHeader Create(CreateLoadingTaskRequests request)
        {
            throw new NotImplementedException();
        }
    }
}
