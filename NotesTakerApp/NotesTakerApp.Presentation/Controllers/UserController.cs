// Updated UserController using UserRepository directly
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;

namespace NotesTakerApp.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _userRepository.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user, string password)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.CreateUserAsync(user, password);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _userRepository.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
