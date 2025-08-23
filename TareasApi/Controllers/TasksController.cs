using Microsoft.AspNetCore.Mvc;
using TareasApi.Domain;
using TareasApi.Repos;

namespace TareasApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TasksController : ControllerBase
{
    private readonly ITasksRepo _tasksRepo;

    public TasksController(ITasksRepo tasksRepo)
    {
        _tasksRepo = tasksRepo;
    }
    
    [HttpGet]
    public IActionResult Get(bool all = false)
    {
        return Ok(_tasksRepo.GetTasks());
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var allTasks = _tasksRepo.GetTasks();
        var oneTask = allTasks.FirstOrDefault(t => t.Id == id);
        
        if (oneTask == null)
        {
            return NotFound();
        }
        
        return Ok(oneTask);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var allTasks = _tasksRepo.GetTasks();
        var oneTask = allTasks.FirstOrDefault(t => t.Id == id);

        if (oneTask == null)
        {
            return NotFound();
        }
        
        allTasks.Remove(oneTask);
        
        return Ok("Deleted");
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] ToDo toDo)
    {
        var allTasks = _tasksRepo.GetTasks();
        allTasks.Add(toDo);
        
        return Ok(toDo);
    }
    
    [HttpPost("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] ToDo toDo)
    {
        var allTasks = _tasksRepo.GetTasks();
        var oneTask = allTasks.FirstOrDefault(t => t.Id == id);
        
        if (oneTask == null)
        {
            return NotFound();
        }

        oneTask.Name = toDo.Name;
        oneTask.IsComplete = toDo.IsComplete;
        oneTask.DueDate = toDo.DueDate;
        
        return Ok(oneTask);
    }
}