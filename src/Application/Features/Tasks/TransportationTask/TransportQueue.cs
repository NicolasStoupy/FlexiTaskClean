using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tasks.TransportationTask
{
    public class TransportQueue
    {
        public int TaskID { get; private set; }
        public string FromArea { get; private set; } = "";
        public string ToArea { get; private set; } = "";
        public string Support { get; private set; } = "";
        public TaskItemStatus TaskItemStatus { get; private set; }
        public string HumanInstruction { get; private set; } = "";
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TransportTask, TransportQueue>()
                    .ForMember(t => t.TaskID,opt=>opt.MapFrom(ti=>ti.TaskItemID))
                    .ForMember(t => t.FromArea, opt => opt.MapFrom(ti => ti.SourceArea.CommonName))
                    .ForMember(t => t.ToArea, opt => opt.MapFrom(ti => ti.DestinationArea.CommonName))
                    .ForMember(t => t.Support, opt => opt.MapFrom(ti => ti.Support))
                    .ForMember(t => t.TaskItemStatus, opt => opt.MapFrom(ti => ti.TaskItemStatus))
                    .ForMember(t => t.HumanInstruction, opt => opt.MapFrom(ti => ti.ToHumanString())
                    );


            }
        }
    }
}
