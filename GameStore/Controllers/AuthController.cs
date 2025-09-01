using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Dtos;
using GameStore.Models;
using GameStore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

    [HttpGet("profile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetProfile()
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized("Invalid token: username not found");
        }
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(new
        {
            Message = "Token is valid",
            UserId = user.Id,
            user.Username,
            user.Email
        });
    }
}
