
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


        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IPasswordHasher<IdentityUser> _passwordHasher;

        public UsersController(eLearningAutomotiveWebSiteContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IPasswordHasher<IdentityUser> passwordHasher)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
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

                var result = await _userManager.CreateAsync(user, model.Password);
                string HostEmail = model.Email.Split('@')[1].Split('.')[0];

                if (result.Succeeded)
                {
                    if(HostEmail == "lamanu")
                    {

                    var roleresult = await _userManager.AddToRoleAsync(user, "Employee");

                        if (roleresult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                        }
 
                    }else
                    { 
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
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
            _signInManager.SignOutAsync();
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
                var result = await _signInManager.PasswordSignInAsync(
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

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> UpdateProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                RegisterUser model = new RegisterUser();
                model.Email = user.Email;

                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(RegisterUser model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);


            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);


            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            model.Email = user.Email;
            return View(model);
        }


        public async Task<IActionResult> ForgottenPassword()
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);

            //if (user != null)
            //{
            //    RegisterUser model = new RegisterUser();
            //    model.Email = user.Email;

            //    return View(model);
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgottenPassword(RegisterUser model)
        {
            if (model == null)
            {
                ModelState.AddModelError("EmailEmpty", "Email ne doit pas être vide");
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("EmailNotFound", "@ Email n'existe pas");
                return View(model);
            }
            else
            {
                RegisterUser newModel = new RegisterUser();
                ViewBag.Email = user.Email;
                return RedirectToAction("ResetPassword");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ResetPassword(RegisterUser model)
        {
                return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(RegisterUser model, int? id)
        {
            if (model == null)
            {
                ModelState.AddModelError("EmailEmpty", "");
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }

}

