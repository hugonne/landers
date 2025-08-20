using TareasApi.Domain;

namespace TareasApi.Repos;

public interface ITasksRepo
{
    IList<ToDo> GetTasks();
}