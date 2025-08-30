using Microsoft.EntityFrameworkCore;
using TareasApi.Domain;

namespace TareasApi.DataAccess;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<ToDoStep> ToDoSteps { get; set; }
}