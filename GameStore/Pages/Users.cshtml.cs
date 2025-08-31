using Microsoft.AspNetCore.Mvc.RazorPages;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Pages;

[Authorize]
public class UserModel : PageModel
{
    public List<User> Accounts { get; set; } = new();

    public void OnGet()
    {
        Accounts = new List<User>
        {
            new User { Username = "admin", Email = "admin@example.com" },
            new User { Username = "user1", Email = "user1@example.com" }
        };
    }
}
