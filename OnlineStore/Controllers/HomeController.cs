using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers
{
    /// <summary>
    /// Главный контроллер Сайта
    /// Осуществляет переход по всем страницам внутри сайта
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Вызов главного представления сайта
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() => View();

        /// <summary>
        /// Вызов представления с Контактами
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact_us() => View();

        /// <summary>
        /// Вызов представления Checkout
        /// </summary>
        /// <returns></returns>
        public IActionResult Checkout() => View();

        /// <summary>
        /// Вызов представления со Статьями
        /// </summary>
        /// <returns></returns>
        public IActionResult Blog_single() => View();

        /// <summary>
        /// Вызов представления с Блогом
        /// </summary>
        /// <returns></returns>
        public IActionResult Blog() => View();

        /// <summary>
        /// Вызов представления с Страница не найдена
        /// </summary>
        /// <returns></returns>
        public IActionResult PageNotFound() => View();

    }
}