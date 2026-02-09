using Domain.Entities.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Interfaces.Tasks
{
    public interface ITaskCreationFacade
    {
        TaskHeader Create<TRequest>(TRequest request)
            where TRequest : class, ITaskCreationRequest;
    }

    public sealed class TaskCreationFacade : ITaskCreationFacade
    {
        private readonly IServiceProvider _sp;

        public TaskCreationFacade(IServiceProvider sp)
        {
            _sp = sp;
        }

        public TaskHeader Create<TRequest>(TRequest request)
            where TRequest : class, ITaskCreationRequest
        {
            var creator = _sp.GetRequiredService<ITaskCreator<TRequest>>();
            return creator.Create(request);
        }
    }
}
