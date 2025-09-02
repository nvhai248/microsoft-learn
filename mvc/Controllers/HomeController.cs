using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc.Interfaces;
using mvc.Models;

namespace mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAppConfigService _config;
    private readonly IRandomService _random1;
    private readonly IRandomService _random2;

    public HomeController(
        ILogger<HomeController> logger,
        IAppConfigService config,
        IRandomService random1,
        IRandomService random2)
    {
        _logger = logger;
        _config = config;
        _random1 = random1;
        _random2 = random2;
    }

    public IActionResult Index()
    {
        ViewBag.AppName = _config.GetAppName();
        return View();
    }

    public IActionResult Privacy()
    {
        ViewBag.Value1 = _random1.GetRandomNumber();
        ViewBag.Value2 = _random2.GetRandomNumber();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
