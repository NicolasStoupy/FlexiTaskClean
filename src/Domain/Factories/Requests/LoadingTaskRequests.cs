using Domain.Common.Interfaces.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories.Requests
{
    public sealed record CreateLoadingTaskRequests() : ITaskCreationRequest
    {

        public string TaskKind => "Loading";
    }
}
