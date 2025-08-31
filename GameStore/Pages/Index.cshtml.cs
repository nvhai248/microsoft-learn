using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameStore.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (Username == "admin" && Password == "123456")
        {
            return RedirectToPage("/Users");
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password");
        return Page();
    }
}
