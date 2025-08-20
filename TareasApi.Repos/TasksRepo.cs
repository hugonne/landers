using TareasApi.Domain;

namespace TareasApi.Repos;

public class TasksRepo : ITasksRepo
{
    private readonly IList<ToDo> _toDos;

    public TasksRepo()
    {
        _toDos = new List<ToDo>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Tarea 1",
                DueDate = DateTime.Now.AddDays(1),
                IsComplete = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Tarea 2",
                DueDate = DateTime.Now.AddDays(2),
                IsComplete = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Tarea 3",
                DueDate = DateTime.Now.AddDays(3),
                IsComplete = true
            }
        };
    }

    public IList<ToDo> GetTasks()
    {
        return _toDos;
    }
}