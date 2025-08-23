using Microsoft.EntityFrameworkCore;
using TareasApi.Domain;

namespace TareasApi.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; }
}