using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.ViewModels;

namespace OnlineStore.Controllers
{
    /// <summary>
    /// Контроллер работы с корзиной
    /// </summary>
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IOrderService orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            this.cartService = cartService;
            this.orderService = orderService;
        }

        public IActionResult Details()
        {
            var model = new CartDetailsViewModel
            {
                CartViewModel = cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };

            return View(model);
        }

        public IActionResult DecrementFromCart(int id)
        {
            cartService.DecrementCountItem(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int id)
        {
            cartService.RemoveFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll()
        {
            cartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddtoCart(int id)
        {
            cartService.AddToCart(id);
            return RedirectToAction("Details");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new CartDetailsViewModel
                {
                    CartViewModel = cartService.TransformCart(),
                    OrderViewModel = orderViewModel
                });

            var order = orderService.CreateNewOrder(orderViewModel, cartService.TransformCart(), User.Identity.Name);


            cartService.RemoveAll();

            return RedirectToAction("OrderConfirmed", new { id = order.id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

    }

}