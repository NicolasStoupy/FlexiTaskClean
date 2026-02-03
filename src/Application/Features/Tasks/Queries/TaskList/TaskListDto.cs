using Application.Plants.Queries.GetPlants;
using Domain.Entities.MasterData;
using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;

namespace Application.Features.Tasks.Queries.TaskList
{
    public class TaskListDto
    {

        public int TaskItemId { get; init; }
        public int TaskHeaderId { get; init; }
        public string TaskItemStatus { get; init; }
        public string TaskItemDescription { get; init; } = null!;

        public string SourceAreaName { get; set; }
        public string DestinationAreaName { get; set; }
        public string? Support { get; set; }



        public TaskListDto() { }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TransportTask, TaskListDto>()
                    .ForMember(d => d.SourceAreaName, opt => opt.MapFrom(s => s.SourceArea.CommonName))
                    .ForMember(d => d.DestinationAreaName, opt => opt.MapFrom(s => s.DestinationArea.CommonName));
  
            }
        }
    }
}