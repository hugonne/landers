using TareasApi.Domain;

namespace TareasApi.Repos;

public interface ITasksRepo
{
    #region ToDos
    
    IQueryable<ToDo> GetTasks(bool all = false);
    ToDo? GetTaskById(Guid id, bool includeSteps = false);
    Guid CreateTask(ToDo toDo);
    void DeleteTaskById(Guid id);
    
    #endregion
    
    #region Steps
    
    Guid CreateStep(ToDoStep toDoStep);
    void DeleteStepById(Guid id);
    void CompleteStepById(Guid id);
    
    #endregion

    void SaveChanges();
}