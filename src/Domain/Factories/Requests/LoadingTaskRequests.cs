using Domain.Common.Interfaces.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories.Requests
{
    public sealed record CreateLoadingTaskRequests(List<LoadingItems> loadingItems) : ITaskCreationRequest
    {
        public string TaskKind => "Loading";

      
    }
    public sealed record LoadingItems()
    {
        public int WorkAreaID { get; set; }
        public string SupportTypeID { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public string Support { get; set;  }

        public int AssignedWorkAreaID { get; set; }
    }
}
