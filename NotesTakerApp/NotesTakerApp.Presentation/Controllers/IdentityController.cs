using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Dtos;
using System.Security.Claims;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.ViewModel;
namespace NotesTakerApp.Presentation.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly EmailSender emailSender;

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, EmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.emailSender = emailSender;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var user = new UserViewModel()
            {
                Email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value!,
                UserName = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value!,
                Roles = User.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(claim => claim.Value).ToString() ?? "User",
                Password = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!
            };

            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password ?? string.Empty);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("All", result.Errors.FirstOrDefault()?.Description ?? "User creation failed.");
                return View(model);
            }

            var foundUser = await userManager.FindByEmailAsync(user.Email);
            if (foundUser == null)
            {
                ModelState.AddModelError("All", "User creation failed.");
                return View(model);
            }

            await userManager.AddToRoleAsync(foundUser, "User");

            var verificationCode = GenerateVerificationCode();

            await userManager.AddClaimAsync(foundUser, new Claim("VerificationCode", verificationCode));

            try
            {
                await emailSender.SendEmailAsync(foundUser.Email, "Your Verification Code", $"Your verification code is: {verificationCode}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("All", "Error sending email: " + ex.Message);
                return View(model);
            }

            return RedirectToAction("VerificationPage");
        }

        [HttpGet]
        public IActionResult VerificationPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(string userId, string verificationCode)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError("All", "User not found.");
                return View();
            }

            var storedCode = (await userManager.GetClaimsAsync(user))
                            .FirstOrDefault(c => c.Type == "VerificationCode")?.Value;

            if (storedCode == null)
            {
                ModelState.AddModelError("All", "Verification code not found.");
                return View();
            }

            if (verificationCode == storedCode)
            {
                await userManager.AddClaimAsync(user, new Claim("EmailVerified", "true"));

                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(Index), "Home");
            }

            ModelState.AddModelError("All", "Invalid verification code.");
            return View();
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                ViewData["returnUrl"] = returnUrl;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var foundUser = await userManager.FindByEmailAsync(model.Email);
            if (foundUser == null)
            {
                ModelState.AddModelError("Email", "User not found.");
                return View(model);
            }

            var signInResult = await signInManager.PasswordSignInAsync(foundUser, model.Password ?? "", true, true);
            if (signInResult.Succeeded)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            ModelState.AddModelError("Password", "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        // Uncomment this method if you need to add roles initially (run only once)
        // [HttpGet]
        // public async Task<IActionResult> AddRoles()
        // {
        //     await roleManager.CreateAsync(new IdentityRole("User"));
        //     await roleManager.CreateAsync(new IdentityRole("Admin"));
        //     var email = "admin@admin";
        //     var result = await userManager.FindByEmailAsync(email);
        //     if (result == null)
        //     {
        //         var admin = new IdentityUser { UserName = "admin", Email = email };
        //         await userManager.CreateAsync(admin, "Admin123!");
        //         await userManager.AddToRoleAsync(admin, "Admin");
        //     }
        //     return Ok();
        // }

        private string GenerateVerificationCode()
        {
            var random = new Random();
            var verificationCode = random.Next(100000, 999999);
            return verificationCode.ToString();
        }
    }
}
