namespace Application.Features.Tasks.Queries.TaskList
{
    public class TaskListVm
    {

        public TaskListVm()
        {
           Tasks = new List<TaskListDto>();
        }
        public List<TaskListDto> Tasks { get; init; }
    }
}