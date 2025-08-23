using TareasApi.Domain;

namespace TareasApi.Repos;

public class TasksRepo : ITasksRepo
{
    private readonly IList<ToDo> _toDos = new List<ToDo>
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

    public IList<ToDo> GetTasks(bool all = false)
    {
        return all ? _toDos.ToList() : _toDos
            .Where(a => !a.IsComplete).ToList();
    }

    public ToDo? GetTaskById(Guid id)
    {
        return _toDos.FirstOrDefault(t => t.Id == id);
    }

    public Guid CreateTask(ToDo toDo)
    {
        toDo.Id = Guid.NewGuid();
        _toDos.Add(toDo);
        return toDo.Id;
    }

    public void DeleteTaskById(Guid id)
    {
        var toDo = GetTaskById(id);
        if (toDo != null)
        {
            _toDos.Remove(toDo);
        }
    }
}