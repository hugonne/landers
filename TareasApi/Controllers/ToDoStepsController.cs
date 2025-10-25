using Microsoft.AspNetCore.Mvc;
using TareasApi.Domain;
using TareasApi.Repos;

namespace TareasApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ToDoStepsController(ITasksRepo tasksRepo) : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] ToDoStep toDoStep)
    {
        var toDo = tasksRepo.GetTaskById(toDoStep.ToDoId);

        if (toDo == null)
        {
            return NotFound();
        }
        
        var id = tasksRepo.CreateStep(toDoStep);
        tasksRepo.SaveChanges();
        
        return Ok(id);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        tasksRepo.DeleteStepById(id);
        tasksRepo.SaveChanges();
        
        return Ok();
    }

    [HttpPost("{id:guid}")]
    public IActionResult Complete(Guid id)
    {
        tasksRepo.CompleteStepById(id);
        tasksRepo.SaveChanges();
        
        return Ok();
    }
}