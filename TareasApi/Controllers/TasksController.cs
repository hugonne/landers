using Microsoft.AspNetCore.Mvc;
using TareasApi.Domain;
using TareasApi.Dtos;
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

        var allTasksDto = allTasks.Select(a => new ToDoResponseDto
        {
            Id = a.Id,
            Name = a.Name,
            IsComplete = a.IsComplete,
            DueDate = a.DueDate,
            StepsCount = a.ToDoSteps.Count(),
            Steps = a.ToDoSteps.Select(b => new ToDoStepDto
            {
                Id = b.Id,
                Description = b.Description,
                IsComplete = b.IsComplete
            })
        });
        
        return Ok(allTasksDto);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var oneTask = tasksRepo.GetTaskById(id, true);
        
        if (oneTask == null)
        {
            return NotFound();
        }

        var newTaskDto = new ToDoResponseDto
        {
            Id = oneTask.Id,
            Name = oneTask.Name,
            IsComplete = oneTask.IsComplete,
            DueDate = oneTask.DueDate,
            StepsCount = oneTask.ToDoSteps.Count(),
            Steps = oneTask.ToDoSteps.Select(a => new ToDoStepDto
            {
                Id = a.Id,
                Description = a.Description,
                IsComplete = a.IsComplete
            })
        };
        
        return Ok(newTaskDto);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        tasksRepo.DeleteTaskById(id);
        tasksRepo.SaveChanges();
        
        return Ok();
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] ToDoRequestDto toDoRequestDto)
    {
        var newToDo = new ToDo()
        {
            Id = Guid.NewGuid(),
            Name = toDoRequestDto.Name,
            DueDate = toDoRequestDto.DueDate
        };

        if (!TryValidateModel(newToDo))
        {
            return BadRequest(ModelState);
        }
        
        var id = tasksRepo.CreateTask(newToDo);
        tasksRepo.SaveChanges();
        
        return Ok(id);
    }
    
    [HttpPost("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] ToDoRequestDto toDoRequestDto)
    {
        var oneTask = tasksRepo.GetTaskById(id);
        
        if (oneTask == null)
        {
            return NotFound();
        }

        oneTask.Name = toDoRequestDto.Name;
        //oneTask.IsComplete = toDoDto.IsComplete;
        oneTask.DueDate = toDoRequestDto.DueDate;
        
        if (!TryValidateModel(oneTask))
        {
            return BadRequest(ModelState);
        }
        
        tasksRepo.SaveChanges();
        
        return Ok(toDoRequestDto);
    }
}