using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GameStore.Dtos;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private static readonly List<GameDto> games = new()
        {
            new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Adventure", 59.99m, new DateOnly(2017, 3, 3)),
            new GameDto(2, "God of War Ragnarök", "Action", 69.99m, new DateOnly(2022, 11, 9)),
            new GameDto(3, "Red Dead Redemption 2", "Open World", 49.99m, new DateOnly(2018, 10, 26)),
            new GameDto(4, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
            new GameDto(5, "Elden Ring", "RPG", 59.99m, new DateOnly(2022, 2, 25)),
            new GameDto(6, "Cyberpunk 2077", "RPG", 39.99m, new DateOnly(2020, 12, 10)),
            new GameDto(7, "Super Mario Odyssey", "Platform", 59.99m, new DateOnly(2017, 10, 27)),
            new GameDto(8, "Hollow Knight", "Metroidvania", 14.99m, new DateOnly(2017, 2, 24)),
            new GameDto(9, "Grand Theft Auto V", "Action", 29.99m, new DateOnly(2013, 9, 17)),
            new GameDto(10, "The Witcher 3: Wild Hunt", "RPG", 39.99m, new DateOnly(2015, 5, 19))
        };

        [HttpGet]
        public IEnumerable<GameDto> GetAll() => games;

        [HttpGet("{id}", Name = "GetGame")]
        public ActionResult<GameDto?> Get(int id)
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            return game is null ? NotFound() : Ok(game);
        }

        [HttpPost]
        public ActionResult<GameDto> Create(CreateGameDto newGame)
        {
            var game = new GameDto(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return CreatedAtRoute("GetGame", new { id = game.Id }, game);
        }
    }
}
