using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Dtos;
using GameStore.Models;
using GameStore.Helpers;

namespace GameStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly GameStoreContext _context;

    public AuthController(GameStoreContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Email already exists");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = PasswordHelper.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registration successful" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !PasswordHelper.VerifyPassword(dto.Password, user.Password))
            return Unauthorized("Invalid credentials");

        var token = JwtHelper.GenerateToken(user.Email, user.Username);

        return Ok(new { token });
    }

    [HttpPost("verify")]
    public IActionResult Verify([FromBody] string token)
    {
        var principal = JwtHelper.VerifyToken(token);
        if (principal == null)
        {
            return Unauthorized("Invalid or expired token");
        }

        return Ok(new
        {
            Message = "Token is valid",
            User = principal.Identity?.Name,
            Claims = principal.Claims.Select(c => new { c.Type, c.Value })
        });
    }

}
