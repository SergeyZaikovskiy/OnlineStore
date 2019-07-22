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

        /// <summary>
        /// Показать детали
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Details()
        {
            var model =  new CartDetailsViewModel
            {
                CartViewModel = await Task.Run(() => cartService.TransformCart()),
                OrderViewModel = new OrderViewModel()
            };

            return View(model);
        }

        /// <summary>
        /// Уменьшить количество товара в корзине
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DecrementFromCart(int id)
        {
            await Task.Run(() => cartService.DecrementCountItem(id));
            return RedirectToAction("Details");
        }

        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        /// <param name="id">ID товара</param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            await Task.Run(() => cartService.RemoveFromCart(id));
            return RedirectToAction("Details");
        }

        /// <summary>
        /// Удалить все товары из корзины
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RemoveAll()
        {
            await Task.Run(() => cartService.RemoveAll());
            return RedirectToAction("Details");
        }

        /// <summary>
        /// Добавить товар в корзину
        /// </summary>
        /// <param name="id">ID товара</param>
        /// <returns></returns>
        public async Task<IActionResult> AddtoCart(int id)
        {
            await Task.Run(() => cartService.AddToCart(id));
            return RedirectToAction("Details");
        }

        /// <summary>
        /// Создать заказ из корзины
        /// </summary>
        /// <param name="orderViewModel">Модель представления заказа</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new CartDetailsViewModel
                {
                    CartViewModel = await Task.Run(() => cartService.TransformCart()),
                    OrderViewModel = orderViewModel
                });

            var order = orderService.CreateNewOrder(orderViewModel, cartService.TransformCart(), User.Identity.Name);


            await Task.Run(() => cartService.RemoveAll());

            return RedirectToAction("OrderConfirmed", new { id = order.id });
        }

        /// <summary>
        /// Подтвердить заказ
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <returns></returns>
        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

    }

}