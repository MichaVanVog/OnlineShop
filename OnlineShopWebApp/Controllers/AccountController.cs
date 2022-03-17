using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersManager userManager;
        public AccountController(IUsersManager userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Login));

            var useraccount = userManager.TryGetByName(login.Username);
            if (useraccount == null)
            {
                ModelState.AddModelError(string.Empty, "Такого пользователя не существует.");
                return RedirectToAction(nameof(Login));
            }

            if (useraccount.Password != login.Password)
            {
                ModelState.AddModelError(string.Empty, "Неправильный пароль");
                return RedirectToAction(nameof(Login));
            }

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
        }

        public IActionResult Registr()
        {
            return View();
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
                userManager.Add(new UserAccount
                {
                    Name = registr.Username,
                    Password = registr.Password,
                    Phone = registr.Phone
                });
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
            }
            return RedirectToAction(nameof(Registr));
        }
    }
}
