using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TareasApi.Domain.Account;
using TareasApi.Dtos;

namespace TareasApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController(
    IConfiguration configuration,
    UserManager<TareasApiUser> userManager,
    RoleManager<TareasApiRole> roleManager,
    SignInManager<TareasApiUser> signInManager) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if (!TryValidateModel(loginDto))
            {
                return BadRequest(ModelState);
            }

            var existingUser = await userManager.FindByNameAsync(loginDto.Username);

            if (existingUser == null)
            {
                return Unauthorized();
            }
            
            var result = await userManager.CheckPasswordAsync(existingUser, loginDto.Password);

            if (!result)
            {
                return Unauthorized();
            }

            await signInManager.SignInAsync(existingUser, true);
            var jwtToken = GenerateJwtSecurityToken(existingUser);
            
            return Ok(jwtToken);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message} {e.InnerException?.Message}");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var user = new TareasApiUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FullName = registerDto.FullName
            };

            if (!TryValidateModel(user))
            {
                return BadRequest(ModelState);
            }

            var result = await userManager.CreateAsync(user, registerDto.Password);
            await userManager.AddToRoleAsync(user, "Admin");

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return BadRequest(string.Join(',', errors));
            }

            await signInManager.SignInAsync(user, true);
            var jwtToken = GenerateJwtSecurityToken(user);

            return Ok(jwtToken);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message} {e.InnerException?.Message}");
        }
    }
    
    private string GenerateJwtSecurityToken(TareasApiUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Email!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Name, user.FullName!),
            new(JwtRegisteredClaimNames.UniqueName, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //Get roles for user and add them to claims
        var roles = userManager.GetRolesAsync(user).Result;
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddYears(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}