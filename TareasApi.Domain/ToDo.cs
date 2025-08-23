using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TareasApi.Domain;

public class ToDo
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string Name { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public bool IsComplete { get; set; }
    
    public ICollection<ToDoStep> ToDoSteps { get; set; }
}