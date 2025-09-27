using Microsoft.EntityFrameworkCore;
using TareasApi.DataAccess;
using TareasApi.Domain;

namespace TareasApi.Repos;

public class TasksRepo(ApplicationDbContext context) : ITasksRepo
{
    public IQueryable<ToDo> GetTasks(bool all = false)
    {
        if (all)
        {
            return context.ToDos.OrderBy(a => a.DueDate);
        }
        
        return context.ToDos.Where(a => !a.IsComplete).OrderBy(a => a.DueDate);
    }

    public ToDo? GetTaskById(Guid id, bool includeSteps = false)
    {
        if (includeSteps)
        {
            return context.ToDos
                .Include(a => a.ToDoSteps)
                .FirstOrDefault(a => a.Id == id);
        }
        
        return context.ToDos.Find(id);
    }

    public Guid CreateTask(ToDo toDo)
    {
        return context.ToDos.Add(toDo).Entity.Id;
    }

    public void DeleteTaskById(Guid id)
    {
        var toDo = GetTaskById(id);
        if (toDo != null)
        {
            context.ToDos.Remove(toDo);
        }
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}