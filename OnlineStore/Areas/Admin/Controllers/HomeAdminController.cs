using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Controllers;
using OnlineStore.Infrastructure.Interfeices;
using SmartBreadcrumbs.Attributes;

namespace OnlineStore.Areas.Admin.Controllers
{
    /// <summary>
    /// Главный контроллер области администрации
    /// </summary>
    [Area("Admin"), Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
    public class HomeAdminController : Controller
    {
        private readonly IProductData productData;

        public HomeAdminController(IProductData productData)
        {
            this.productData = productData;
        }

        /// <summary>
        /// Стартовая страница области администратора
        /// </summary>
        /// <returns></returns>
        [Breadcrumb("Область администратора")]
        public IActionResult Index() => View();

        /// <summary>
        /// Внутренняя страница области администратора
        /// </summary>
        /// <returns></returns>
        [Breadcrumb("Информация", FromAction = "Index", FromController = typeof(HomeAdminController))]
        public IActionResult Info() => View();

    }

}