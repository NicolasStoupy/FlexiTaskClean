using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Interfaces.Tasks
{
    public interface ITaskCreationRequest
    {
        string TaskKind { get; }   // ou enum TaskKind
    }

}
