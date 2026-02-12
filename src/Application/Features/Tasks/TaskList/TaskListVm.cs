namespace Application.Features.Tasks.TaskList
{
    public class TaskListVm
    {
        public TaskListVm()
        {
            Tasks = new List<TaskListItemDto>();
        }

        public List<TaskListItemDto> Tasks { get; init; }
    }
}