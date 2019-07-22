using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.Models;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Implementations
{
    /// <summary>
    /// Реализация сервиса Корзины
    /// </summary>
    public class CookieCartService : ICartService
    {
        private readonly IProductData productData;
        private readonly IHttpContextAccessor httpContextAccessor;
        readonly string CartName;

        /// <summary>
        /// Настройки корзины
        /// </summary>
        private Cart Cart
        {
            get
            {
                var http_context = httpContextAccessor.HttpContext;
                var cookie = http_context.Request.Cookies[CartName];

                Cart cart = null;

                if (cookie is null)
                {
                    cart = new Cart();
                    http_context.Response.Cookies.Append(
                        CartName, JsonConvert.SerializeObject(cart));

                }
                else
                {
                    cart = JsonConvert.DeserializeObject<Cart>(cookie);

                    http_context.Response.Cookies.Delete(CartName);
                    http_context.Response.Cookies.Append(CartName, cookie, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(2)
                    });
                }
                return cart;
            }

            set
            {
                var http_context = httpContextAccessor.HttpContext;
                var json = JsonConvert.SerializeObject(value);

                http_context.Response.Cookies.Delete(CartName);
                http_context.Response.Cookies.Append(CartName, json, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(2)
                });
            }
        }

        /// <summary>
        /// Зависимости
        /// </summary>
        /// <param name="productData">Сервис для работы с товарами</param>
        /// <param name="httpContextAccessor">Сервис для доступа к контексту запроса</param>
        public CookieCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            this.productData = productData;
            this.httpContextAccessor = httpContextAccessor;


            var user = httpContextAccessor.HttpContext.User;
            var user_name = user.Identity.IsAuthenticated ? user.Identity.Name : null;

            CartName = $"cart{user_name}";
        }

        /// <summary>
        /// Добавить товар в корзину
        /// </summary>
        /// <param name="id">id товара</param>
        public void AddToCart(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item != null)
                item.Quantity++;
            else
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });

            Cart = cart;
        }

        /// <summary>
        /// Уменьшить кол-ва товара в корзине
        /// </summary>
        /// <param name="id">id товара</param>
        public void DecrementCountItem(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item != null)
            {
                if (item.Quantity > 0)
                    item.Quantity--;
                else
                    cart.Items.Remove(item);

                Cart = cart;

            }
        }

        /// <summary>
        /// Удалить все
        /// </summary>
        public void RemoveAll()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        /// <param name="id">id товара</param>
        public void RemoveFromCart(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item != null)
            {
                cart.Items.Remove(item);
                Cart = cart;
            }
        }

        /// <summary>
        /// Сформировать корзину
        /// </summary>
        /// <returns></returns>
        public CartViewModel TransformCart()
        {
            var products = productData.GetProducts(new ProductFilter
            {
                Identifocators = Cart.Items.Select(item => item.ProductId).ToList()
            });

            var products_view_models = products.Select(p => p.CreateViewModel());

            return new CartViewModel
            {
                Items = Cart.Items.ToDictionary(x => products_view_models.First(p => p.Id == x.ProductId), x => x.Quantity)
            };
        }
    }

}
