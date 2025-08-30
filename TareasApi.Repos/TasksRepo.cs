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

    public ToDo? GetTaskById(Guid id)
    {
        var x = context.ToDoSteps
            .Include(a => a.ToDo)
            .Where(a => a.ToDo.IsComplete && !a.IsComplete)
            //.Select(a => a.ToDo)
            .Distinct();
        
        return context.ToDos.FirstOrDefault(a => a.Id == id);
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