using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;
using NotesTakerApp.Core.Services;

namespace NotesTakerApp.Presentation.Controllers
{
    public class UserController : Controller
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
            return View("Index", users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await userService.AddUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                await userService.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
