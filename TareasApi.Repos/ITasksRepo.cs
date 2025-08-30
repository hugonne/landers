using TareasApi.Domain;

namespace TareasApi.Repos;

public interface ITasksRepo
{
    IQueryable<ToDo> GetTasks(bool all = false);
    ToDo? GetTaskById(Guid id);
    Guid CreateTask(ToDo toDo);
    void DeleteTaskById(Guid id);
    void SaveChanges();
}