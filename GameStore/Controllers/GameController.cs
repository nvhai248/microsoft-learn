using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Dtos;
using GameStore.Models;

namespace GameStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly GameStoreContext _context;

    public GamesController(GameStoreContext context)
    {
        _context = context;
    }

    // GET api/games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetAll()
    {
        var games = await _context.Games
            .Select(g => new GameDto(
                g.Id,
                g.Name,
                g.Genre,
                g.Price,
                g.ReleaseDate
            ))
            .ToListAsync();

        return Ok(games);
    }

    // GET api/games/5
    [HttpGet("{id}", Name = "GetGame")]
    public async Task<ActionResult<GameDto>> Get(int id)
    {
        var game = await _context.Games.FindAsync(id);

        if (game is null)
            return NotFound();

        var dto = new GameDto(
            game.Id,
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate
        );

        return Ok(dto);
    }

    // POST api/games
    [HttpPost]
    public async Task<ActionResult<GameDto>> Create(CreateGameDto newGame)
    {
        var game = new Game
        {
            Name = newGame.Name,
            Genre = newGame.Genre,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var dto = new GameDto(
            game.Id,
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate
        );

        return CreatedAtRoute("GetGame", new { id = game.Id }, dto);
    }
}
