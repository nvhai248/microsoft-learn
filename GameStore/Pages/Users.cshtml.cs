using Microsoft.AspNetCore.Mvc.RazorPages;
using GameStore.Models;
using GameStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Pages;

[Authorize]
public class UserModel : PageModel
{
    private readonly GameStoreContext _context;

    [ActivatorUtilitiesConstructor]
    public UserModel(GameStoreContext context)
    {
        _context = context;
    }

    public List<User> Accounts { get; set; } = new();

    public async Task OnGetAsync()
    {
        Accounts = await _context.Users.ToListAsync();
    }
}
