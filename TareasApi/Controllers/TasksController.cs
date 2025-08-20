using Microsoft.AspNetCore.Mvc;
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
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        return Ok("One: " + id);
    }
    
    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok("Deleted");
    }
    
    [HttpPost]
    public IActionResult Create()
    {
        return Ok("Deleted");
    }
    
    [HttpPost]
    public IActionResult Update()
    {
        return Ok("Deleted");
    }
}