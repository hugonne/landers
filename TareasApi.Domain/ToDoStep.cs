using System.ComponentModel.DataAnnotations;

namespace TareasApi.Domain;

public class ToDoStep
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string Description { get; set; }
    
    public bool IsComplete { get; set; }
    
    public Guid ToDoId { get; set; }
    public ToDo ToDo { get; set; }
}