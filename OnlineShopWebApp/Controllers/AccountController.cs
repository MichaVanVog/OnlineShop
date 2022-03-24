using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUsersManager usersManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(IUsersManager usersManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.usersManager = usersManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new Login() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(login.Username, login.Password, login.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(login.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неправильный пароль");
                }
            }
            return View(login);
        }

        public IActionResult Registr(string returnUrl)
        {
            return View(new Registr() { ReturnUrl = returnUrl});
        }

        [HttpPost]
        public IActionResult Registr(Registr registr)
        {
            if (registr.Username == registr.Password)
            {
                ModelState.AddModelError(string.Empty, "Логин и пароль не должны совпадать!");
            }
            if (ModelState.IsValid)
            {
                User user = new() { Email = registr.Username, UserName = registr.Username };
                var result = _userManager.CreateAsync(user, registr.Password).Result;
                if (result.Succeeded)
                {
                    _signInManager.SignInAsync(user, false);
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, "Логин не возможен");
                    }
                }
            }
            return View(registr);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
