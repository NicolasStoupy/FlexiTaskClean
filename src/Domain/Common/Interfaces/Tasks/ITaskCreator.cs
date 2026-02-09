using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Interfaces.Tasks
{
    public interface ITaskCreator<in TRequest> where TRequest : ITaskCreationRequest
    {
        TaskHeader Create(TRequest request);
    }
}
