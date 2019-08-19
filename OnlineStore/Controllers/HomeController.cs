using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

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
        [Breadcrumb("Главная")]
        public IActionResult Index() => View();

        /// <summary>
        /// Вызов представления с Контактами
        /// </summary>
        /// <returns></returns>
        [Breadcrumb("Связаться с нами")]
        public IActionResult Contact_us() => View();

        /// <summary>
        /// Вызов представления Checkout
        /// </summary>
        /// <returns></returns>
        [Breadcrumb("Регистрация/вход")]
        public IActionResult Checkout() => View();

        /// <summary>
        /// Вызов представления со Статьями
        /// </summary>
        /// <returns></returns>
        [Breadcrumb("Статья")]
        public IActionResult Blog_single() => View();

        /// <summary>
        /// Вызов представления с Блогом
        /// </summary>
        /// <returns></returns>
        [Breadcrumb("Блог")]
        public IActionResult Blog() => View();

        /// <summary>
        /// Вызов представления с Страница не найдена
        /// </summary>
        /// <returns></returns>
        [Breadcrumb("Страница не найдена")]
        public IActionResult PageNotFound() => View();

    }
}