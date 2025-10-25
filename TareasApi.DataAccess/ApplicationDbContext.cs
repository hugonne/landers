using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TareasApi.Domain;
using TareasApi.Domain.Account;

namespace TareasApi.DataAccess;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<TareasApiUser, TareasApiRole, Guid>(options)
{
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<ToDoStep> ToDoSteps { get; set; }
}