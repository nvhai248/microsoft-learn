using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using GameStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Pages;

[Authorize]
public class GamesModel : PageModel
{
    private readonly GameStoreContext _context;

    [ActivatorUtilitiesConstructor]
    public GamesModel(GameStoreContext context)
    {
        _context = context;
    }

    public List<Game> Games { get; set; } = new();

    // ✅ New game for binding form

    [BindProperty]
    public Game NewGame { get; set; } = new();

    public async Task OnGetAsync()
    {
        Games = await _context.Games.ToListAsync();
    }

    // ✅ handle when click Add Game
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Games = await _context.Games.ToListAsync();
            return Page();
        }

        _context.Games.Add(NewGame);
        await _context.SaveChangesAsync();

        return RedirectToPage(); // reload
    }
}
