namespace TareasApi.Dtos;

public class ToDoResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public int StepsCount { get; set; }
    public bool IsComplete { get; set; }
    public IEnumerable<ToDoStepDto> Steps { get; set; }
}