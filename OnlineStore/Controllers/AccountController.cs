using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities.UserEntities;
using OnlineStore.ViewModels;

namespace OnlineStore.Controllers
{

    /// <summary>
    /// Контроллер для действий с Логином и Регистрацией
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManger;
        private readonly SignInManager<User> signInManager;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="userManger"></param>
        /// <param name="signInManager"></param>
        public AccountController(UserManager<User> userManger, SignInManager<User> signInManager)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Вызов представления Login Get-request
        /// </summary>
        /// <returns></returns>
        public IActionResult Login() => View();

        /// <summary>
        /// Отправка представления Login Post-request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var login_result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (login_result.Succeeded)
            {
                if (Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Логин или пароль указаны не верно!");

            return View(model);
        }

        /// <summary>
        /// Выход из системы логина, перенаправляется на представление Index (стартовая страница) Контроллера Home 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Вызов представления Register Get-request
        /// </summary>
        /// <returns></returns>
        public IActionResult Register() => View();

        /// <summary>
        /// Отправка представления Register Post-request
        /// </summary>
        /// <param name="model">Модель регистрации пользователя</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var new_user = new User
            {
                UserName = model.UserName
            };

            var creation_result = await userManger.CreateAsync(new_user, model.Password);
            if (creation_result.Succeeded)
            {
                await signInManager.SignInAsync(new_user, false);

                return RedirectToAction("index", "Home");
            }
            else
            {
                foreach (var error in creation_result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

        }

        /// <summary>
        /// Вызов представление AccessDenied (доступ отклонен)
        /// </summary>
        /// <returns></returns>
        public IActionResult AccessDenied() => View();
    }

}