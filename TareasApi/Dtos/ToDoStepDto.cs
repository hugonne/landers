namespace TareasApi.Dtos;

public class ToDoStepDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
}