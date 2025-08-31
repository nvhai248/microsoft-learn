using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GameStore.Pages;

[Authorize]
public class ProfileModel : PageModel
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public void OnGet()
    {
        // get from Claims
        Username = User.FindFirstValue(ClaimTypes.Name) ?? "";
        Email = User.FindFirstValue(ClaimTypes.Email) ?? "";
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage("/Index");
    }
}
