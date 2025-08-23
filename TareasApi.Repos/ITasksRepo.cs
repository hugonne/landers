using TareasApi.Domain;

namespace TareasApi.Repos;

public interface ITasksRepo
{
    IList<ToDo> GetTasks(bool all = false);
    ToDo? GetTaskById(Guid id);
    Guid CreateTask(ToDo toDo);
    void DeleteTaskById(Guid id);
}