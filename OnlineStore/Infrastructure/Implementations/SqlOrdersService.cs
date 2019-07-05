using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Domain.Entities.UserEntities;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Implementations
{
    /// <summary>
    /// Реализация сервиса работы с Заказами и базой данных
    /// </summary>
    public class SqlOrdersService : IOrderService
    {
        private readonly OnlineStoreContext db;
        private readonly UserManager<User> userManager;

        public SqlOrdersService(OnlineStoreContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        /// <summary>
        /// Создать новый заказ
        /// </summary>
        /// <param name="OrderViewModel"></param>
        /// <param name="CartViewModel"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public Order CreateNewOrder(OrderViewModel OrderViewModel, CartViewModel CartViewModel, string UserName)
        {
            var user = userManager.FindByNameAsync(UserName).Result;

            using (var transaction = db.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Name = OrderViewModel.Name,
                    Phone = OrderViewModel.Phone,
                    Address = OrderViewModel.Address,
                    User = user,
                    Date = DateTime.Now
                };

                db.Order.Add(order);

                foreach (var (product_model, count) in CartViewModel.Items)
                {
                    var product = db.Products.FirstOrDefault(p => p.id == product_model.Id);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с таким идентификатором {product_model.Id} в базе данных не найден");

                    var order_item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = count
                    };
                    db.OrderItems.Add(order_item);
                }

                db.SaveChanges();
                transaction.Commit();

                return order;
            }
        }

        /// <summary>
        /// Получить заказ из базы
        /// </summary>
        /// <param name="Id">ID заказа</param>
        /// <returns></returns>
        public Order GetOrderById(int Id) => db.Order.Include(o => o.OrderItems).FirstOrDefault(o => o.id == Id);

        /// <summary>
        /// Получить все заказы пользователя
        /// </summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns></returns>
        public IEnumerable<Order> GetUserOrders(string UserName) => db.Order
                                                                     .Include(order => order.User)
                                                                     .Include(order => order.OrderItems)
                                                                     .Where(order => order.User.UserName == UserName)
                                                                     .ToArray();


    }
}
