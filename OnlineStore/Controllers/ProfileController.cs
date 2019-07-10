using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.ViewModels;

namespace OnlineStore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IOrderService orderService;

        public ProfileController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Index() => View();

        public IActionResult Orders()
        {
            var orders = orderService.GetUserOrders(User.Identity.Name);

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
