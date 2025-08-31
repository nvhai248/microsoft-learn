using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using GameStore.Data;
using GameStore.Models;
using GameStore.Helpers;

namespace GameStore.Pages;

public class IndexModel : PageModel
{
    private readonly GameStoreContext _context;

    public IndexModel(GameStoreContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public bool ShowRegister { get; set; }

    [BindProperty] public string LoginUsername { get; set; } = string.Empty;
    [BindProperty] public string LoginPassword { get; set; } = string.Empty;

    [BindProperty] public string RegisterUsername { get; set; } = string.Empty;
    [BindProperty] public string RegisterEmail { get; set; } = string.Empty;
    [BindProperty] public string RegisterPassword { get; set; } = string.Empty;

    public IActionResult OnGet(bool register = false)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Games");
        }

        ShowRegister = register;
        return Page();
    }

    // ✅ Handle login
    public async Task<IActionResult> OnPostLoginAsync()
    {
        var user = await _context.Admins.FirstOrDefaultAsync(u => u.Username == LoginUsername);
        if (user == null || !PasswordHelper.VerifyPassword(LoginPassword, user.Password))
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return Page();
        }

        // create claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // sign in -> create cookie
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                IsPersistent = true, // "Remember me"
                ExpiresUtc = DateTime.UtcNow.AddHours(1)
            });

        return RedirectToPage("/Games");
    }

    // ✅ Handle register
    public async Task<IActionResult> OnPostRegisterAsync()
    {
        if (await _context.Admins.AnyAsync(u => u.Username == RegisterUsername))
        {
            ModelState.AddModelError(string.Empty, "Username already taken");
            return Page();
        }

        var admin = new Admin
        {
            Username = RegisterUsername,
            Email = RegisterEmail,
            Password = PasswordHelper.HashPassword(RegisterPassword)
        };

        _context.Admins.Add(admin);
        await _context.SaveChangesAsync();

        // redirect to login
        return RedirectToPage("/Index", new { register = false });
    }

    // ✅ Handle logout
    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage("/Index");
    }
}
