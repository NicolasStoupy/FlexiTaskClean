using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public interface IWorkflowGenerator<in TRequest>
    {
        TaskHeader Create(TRequest request);
    }
}
