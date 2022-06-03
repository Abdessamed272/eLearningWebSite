
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eLearningAutomotiveWebSite.Data;
using eLearningAutomotiveWebSite.Models;
using Microsoft.AspNetCore.Identity;

namespace eLearningAutomotiveWebSite.Controllers
{
    public class UsersController : Controller
    {
        private readonly eLearningAutomotiveWebSiteContext _context;


        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsersController(eLearningAutomotiveWebSiteContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

            
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
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
        [ValidateAntiForgeryToken]

        public IActionResult Logout()
        {
             signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginUser model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login");
            }

            return View(model);
        }

    }
}
