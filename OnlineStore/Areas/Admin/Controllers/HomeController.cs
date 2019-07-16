using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Interfeices;

namespace OnlineStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
    public class HomeController : Controller
    {
        private readonly IProductData productData;

        public HomeController(IProductData productData)
        {
            this.productData = productData;
        }

        /// <summary>
        /// Стартовая страница области администратора
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() => View();

    }

}