using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.ViewModels;
using SmartBreadcrumbs.Attributes;

namespace OnlineStore.Controllers
{
    /// <summary>
    /// Контроллер профиля пользователя
    /// </summary>
    public class ProfileController : Controller
    {
        private readonly IOrderService orderService;

        public ProfileController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [Breadcrumb("Профиль", FromAction = "Index", FromController = typeof(HomeController))]
        public IActionResult Index() => View();

        /// <summary>
        /// Получить заказы пользователя
        /// </summary>
        /// <returns></returns>
        [Breadcrumb("Заказы")]
        public async Task<IActionResult> Orders()
        {
            var orders = await orderService.GetUserOrders(User.Identity.Name).AsNoTracking().ToListAsync();

            return View(orders.Select(order => new UserOrderViewModel
            {
                Id = order.id,
                Name = order.Name,
                Phone = order.Phone,
                Address = order.Address,
                TotalSum = order.OrderItems.Sum(o => o.Quantity * o.Price)
            }));
        }
    }

}
