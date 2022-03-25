using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
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
                    return Redirect(login.ReturnUrl ?? $"/{nameof(HomeController).Replace("Controller", "")}");
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
            return View(new Registr() { ReturnUrl = returnUrl });
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
                User user = new() { Email = registr.Username, UserName = registr.Username, PhoneNumber = registr.Phone };
                var result = _userManager.CreateAsync(user, registr.Password).Result;
                if (result.Succeeded)
                {
                    _signInManager.SignInAsync(user, false);
                    TryAssignUserRole(user);
                    return Redirect(registr.ReturnUrl ?? $"/{nameof(HomeController).Replace("Controller", "")}");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(registr);
        }

        private void TryAssignUserRole(User user)
        {
            try
            {
                _userManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
            }
            catch (Exception)
            {

                //log
            }
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
