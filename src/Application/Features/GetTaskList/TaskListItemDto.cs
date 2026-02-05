using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;

namespace Application.Features.GetTaskList
{
    public class TaskListItemDto
    {
        public int TaskItemID { get; init; }
        public int TaskHeaderID { get; init; }
        public string TaskItemStatus { get; init; }
        public string TaskItemType { get; init; } = null!;
        public DateOnly TargetDate { get; init; }


        public TaskListItemDto() { }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TransportTask, TransportationTaskListItemDto>()
                    .ForMember(d => d.SourceAreaName, opt => opt.MapFrom(s => s.SourceArea.CommonName))
                    .ForMember(d => d.DestinationAreaName, opt => opt.MapFrom(s => s.DestinationArea.CommonName))
                    .ForMember(d => d.Support, opt => opt.MapFrom(s => s.Support));

                CreateMap<LoadingTask, LoadingTaskListItemDto>()
                    .ForMember(d => d.Material, opt => opt.MapFrom(s => s.Material))
                    .ForMember(d => d.AreaForLoadingName, opt => opt.MapFrom(s => s.AreaForLoading.CommonName))
                    .ForMember(d => d.Support, opt => opt.MapFrom(s => s.Support));


                CreateMap<TaskItem, TaskListItemDto>()
                    .Include<TransportTask, TransportationTaskListItemDto>()
                    .ForMember(d => d.TaskItemID, opt => opt.MapFrom(s => s.TaskItemID))
                    .ForMember(d => d.TaskHeaderID, opt => opt.MapFrom(s => s.TaskHeaderID))
                    .ForMember(d => d.TaskItemStatus, opt => opt.MapFrom(s => s.TaskItemStatus.ToString()))
                    .ForMember(d => d.TaskItemType, opt => opt.MapFrom(s => s.GetType().Name))               
                    .Include<LoadingTask, LoadingTaskListItemDto>()
                    .ForMember(d => d.TaskItemID, opt => opt.MapFrom(s => s.TaskItemID))
                    .ForMember(d => d.TaskHeaderID, opt => opt.MapFrom(s => s.TaskHeaderID))
                    .ForMember(d => d.TaskItemStatus, opt => opt.MapFrom(s => s.TaskItemStatus.ToString()))
                    .ForMember(d => d.TaskItemType, opt => opt.MapFrom(s => s.GetType().Name));


            }
        }
    }
    public class TransportationTaskListItemDto : TaskListItemDto
    {
        public string SourceAreaName { get; init; } = "";
        public string DestinationAreaName { get; init; } = "";
        public string? Support { get; init; }
    }
    public class LoadingTaskListItemDto : TaskListItemDto
    {
        public string Material { get; init; } = null!;
        public double Quantity { get; init; }
        public string? Support { get; init; }
        public string AreaForLoadingName { get; init; }
    }

}