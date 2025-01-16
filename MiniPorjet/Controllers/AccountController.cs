using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniPorjet.Context;
using MiniPorjet.Models;
using MiniPorjet.ViewModels;
using System.Data;

namespace MiniPorjet.Controllers
{
    public class AccountController : Controller
    {
        // GET: AccountController
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser>         signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<IdentityUser>
        userManager, SignInManager<IdentityUser> signInManager , ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;

        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)

        {
            if (ModelState.IsValid)
            {

                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser

                {
                    UserName = model.Email,
                    Email = model.Email,
                    // Associer le numéro de téléphone à l'utilisateur
                    PhoneNumber = model.Telephone,
                };
                // Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, model.Password);
                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController

                if (result.Succeeded)


                {
                    // Créer un client et lier le téléphone
                    var client = new Client
                    {
                        ClientName = user.UserName, // Par exemple, utiliser le nom de l'utilisateur
                        ClientAdresse = "Default Address", // Adresse par défaut, à ajuster selon les besoins
                        ClientTelephone = model.Telephone
                    };

                    _context.Clients.Add(client);
                    await _context.SaveChangesAsync();


                    // Par défaut, assignez le rôle "Client"
                    await userManager.AddToRoleAsync(user, "Client");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)

                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Logout()

        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email,
                model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }
    }
}