using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.ViewModels;

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

        public IActionResult Index() => View();

        /// <summary>
        /// Получить заказы пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Orders()
        {
            var orders = await orderService.GetUserOrders(User.Identity.Name).ToListAsync();

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
