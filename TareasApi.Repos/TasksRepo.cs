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
            return context.ToDos
                .Include(a => a.ToDoSteps)
                .OrderBy(a => a.DueDate);
        }

        return context.ToDos
            .Include(a => a.ToDoSteps)
            .Where(a => !a.IsComplete).OrderBy(a => a.DueDate);
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
        var toDo = context.ToDos.Find(id);
        if (toDo != null)
        {
            context.ToDos.Remove(toDo);
        }
    }

    public Guid CreateStep(ToDoStep toDoStep)
    {
        return context.ToDoSteps.Add(toDoStep).Entity.Id;
    }

    public void DeleteStepById(Guid id)
    {
        var toDoStep = context.ToDoSteps.Find(id);
        if (toDoStep != null)
        {
            context.ToDoSteps.Remove(toDoStep);
        }
    }

    public void CompleteStepById(Guid id)
    {
        var toDoStep = context.ToDoSteps.Find(id);

        if (toDoStep == null)
        {
            return;
        }
        
        toDoStep.IsComplete = true;
        
        var toDo = context.ToDos.Include(a => a.ToDoSteps).First(a => a.Id == toDoStep.ToDoId);
        
        if (toDo.ToDoSteps.All(a => a.IsComplete))
        {
            
            toDo.IsComplete = true;
        }
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}