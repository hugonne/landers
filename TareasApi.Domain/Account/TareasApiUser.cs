using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TareasApi.Domain.Account;

public class TareasApiUser : IdentityUser<Guid>
{
    [Required]
    [MaxLength(256)]
    public string FullName { get; set; }
    
    [MaxLength(1024)]
    public string Avatar { get; set; }
}