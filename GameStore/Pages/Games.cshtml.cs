using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using GameStore.Data;

namespace GameStore.Pages;

public class GamesModel : PageModel
{
    private readonly GameStoreContext _context;

    [ActivatorUtilitiesConstructor]
    public GamesModel(GameStoreContext context)
    {
        _context = context;
    }

    public List<Game> Games { get; set; } = new();

    public async Task OnGetAsync()
    {
        Games = await _context.Games.ToListAsync();
    }
}
