using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Models;
using GameStore.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GameStore.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WishListController : ControllerBase
{
    private readonly GameStoreContext _context;

    public WishListController(GameStoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> WishList()
    {
        var username = User.Identity?.Name;
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var wishlist = await _context.WishList
       .Where(w => w.UserId == user.Id)
       .Include(w => w.Game)
       .Select(w => new
       {
           w.GameId,
           Game = w.Game,
           w.WishCount
       })
       .ToListAsync();

        return Ok(wishlist);
    }

    // POST: api/WishList/wish
    [HttpPost("wish")]
    public async Task<IActionResult> WishGame([FromBody] WishListDto request)
    {
        var username = User.Identity?.Name;
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
        {
            return NotFound("User not found");
        }

        if (!await _context.Games.AnyAsync(g => g.Id == request.GameId))
            return NotFound($"Game with id {request.GameId} not found.");

        if (!await _context.Users.AnyAsync(u => u.Id == user.Id))
            return NotFound($"User with id {user.Id} not found.");

        var wish = await _context.WishList
            .FirstOrDefaultAsync(w => w.UserId == user.Id && w.GameId == request.GameId);

        if (wish is null)
        {
            wish = new WishList
            {
                UserId = user.Id,
                GameId = request.GameId,
                WishCount = 1
            };
            _context.WishList.Add(wish);
        }
        else
        {
            wish.WishCount += 1;
            _context.WishList.Update(wish);
        }

        await _context.SaveChangesAsync();
        return Ok(new
        {
            wish.GameId,
            wish.UserId,
            wish.WishCount
        });
    }

    // DELETE: api/WishList/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteWish([FromBody] WishListDto request)
    {

        var username = User.Identity?.Name;
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var wish = await _context.WishList
            .FirstOrDefaultAsync(w => w.UserId == user.Id && w.GameId == request.GameId);

        if (wish is null)
            return NotFound("Wish not found.");

        _context.WishList.Remove(wish);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
