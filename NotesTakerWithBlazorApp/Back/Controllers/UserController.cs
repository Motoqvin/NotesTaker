using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Back.Models;
using Back.Services.Base;

namespace NotesTakerApp.Presentation.Controllers;
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (ModelState.IsValid)
        {
            await userService.AddUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

        var allUsers = await userService.GetAllUsersAsync();
        return RedirectToAction("Index", allUsers);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(User user)
    {
        if (ModelState.IsValid)
        {
            await userService.UpdateUserAsync(user);
            return RedirectToAction(nameof(Index));
        }
        return Ok(user);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await userService.DeleteUserAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
