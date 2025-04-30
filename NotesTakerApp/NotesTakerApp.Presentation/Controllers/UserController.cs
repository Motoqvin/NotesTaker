using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Models;
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
            var viewModel = new UserViewModel
            {
                AllUsers = users
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await userService.AddUserAsync(viewModel.NewUser);
                return RedirectToAction(nameof(Index));
            }

            viewModel.AllUsers = await userService.GetAllUsersAsync();
            return View("Index", viewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
