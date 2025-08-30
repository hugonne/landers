using Microsoft.AspNetCore.Mvc;
using TareasApi.Domain;
using TareasApi.Repos;

namespace TareasApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TasksController(ITasksRepo tasksRepo) : ControllerBase
{
    [HttpGet]
    public IActionResult Get(bool all = false)
    {
        var allTasks = tasksRepo.GetTasks(all);
        return Ok(allTasks);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var oneTask = tasksRepo.GetTaskById(id);
        
        if (oneTask == null)
        {
            return NotFound();
        }
        
        return Ok(oneTask);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        tasksRepo.DeleteTaskById(id);
        tasksRepo.SaveChanges();
        
        return Ok("Deleted");
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] ToDo toDo)
    {
        var id = tasksRepo.CreateTask(toDo);
        return Ok(id);
    }
    
    [HttpPost("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] ToDo toDo)
    {
        var oneTask = tasksRepo.GetTaskById(id);
        
        if (oneTask == null)
        {
            return NotFound();
        }

        oneTask.Name = toDo.Name;
        oneTask.IsComplete = toDo.IsComplete;
        oneTask.DueDate = toDo.DueDate;
        
        return Ok(oneTask);
    }

    public void Hola()
    {
        var step = new ToDoStep();
        var toDoDueDate1 = step.ToDo.DueDate;

        var todo = tasksRepo.GetTaskById(step.ToDoId);
        var stepCount = todo.ToDoSteps.Count;
    }
}