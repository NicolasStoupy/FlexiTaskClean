using Domain.Common.Interfaces.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories.Requests
{
    public record EmptySupportTaskRequest(string supportTypeID, int quantity, string? comment,int destinationWorkAreaID) : ITaskCreationRequest
    {
        public string TaskKind => "Empty support";
    }
}
