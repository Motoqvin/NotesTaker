using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Dtos;
using NotesTakerApp.Core.Enums;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Infrastructure.Data;

namespace NotesTakerApp.Presentation.Controllers;
[Route("[controller]/[action]")]
public class IdentityController : Controller
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UsersIdentityDb dbContext;

    public IdentityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, UsersIdentityDb dbContext)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Register()
    {
        var errorMessage = base.TempData["Error"];

        if(errorMessage != null) {
            base.ModelState.AddModelError("All", errorMessage.ToString()!);
        }

        return base.View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] User newUser){
        try {
            var user = new IdentityUser {
                UserName = newUser.Username,
                Email = newUser.Email,
            };
            await this.userManager.CreateAsync(user, newUser.Password!);

            await this.userManager.AddToRolesAsync(user, new List<string> {
                nameof(Roles.Admin),
                nameof(Roles.User),
            });
        
        }
        catch(Exception) {
            base.TempData["Error"] = "Something went wrong...";
            return base.RedirectToAction(nameof(Register));
        }

        return base.RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        var errorMessage = base.TempData["Error"];

        if(string.IsNullOrWhiteSpace(returnUrl) == false) {
            base.ViewData["returnUrl"] = returnUrl;
        }

        if(errorMessage != null) {
            base.ModelState.AddModelError("All", errorMessage.ToString()!);
        }

        return base.View();
    }

    [HttpPost]
    public async Task<ActionResult> Login([FromForm] LoginDto dto) {
        var foundUser = await this.userManager.FindByEmailAsync(dto.Login);

        if(foundUser == null) {
            base.TempData["Error"] = "Incorrect login or password";
            return base.RedirectToAction(actionName: nameof(Login));
        }

        var signInResult = await signInManager.PasswordSignInAsync(foundUser, dto.Password, true, true);

        if(signInResult.Succeeded == false) {
            base.TempData["Error"] = "Incorrect login or password";
            return base.RedirectToAction(actionName: nameof(Login));
        }

        if(string.IsNullOrWhiteSpace(dto.ReturnUrl) == false) {
            return base.Redirect(dto.ReturnUrl);
        }

        return base.RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpGet]
    public async Task<ActionResult> Logout([FromForm] LoginDto dto) {
        await signInManager.SignOutAsync();

        return base.RedirectToAction(actionName: "Index", controllerName: "Home");
    }
}