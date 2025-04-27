using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Presentation.Models;

namespace NotesTakerApp.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]
    public IActionResult MyInfo()
    {
        var user = new User {
            Id = int.Parse(base.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!),
            Username = base.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value!,
            Email = base.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value!,
        };

        return base.View(user);
    }

    [Authorize(policy: "MyPolicy")]
    public IActionResult GetAllUsers() {
        return View("AllUsers");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
