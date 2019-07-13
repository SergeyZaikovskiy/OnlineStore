using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Interfeices
{
    /// <summary>
    /// Интерфейс для работы с заказами
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Получить список заказов
        /// </summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns></returns>
        IQueryable<Order> GetUserOrders(string UserName);

        /// <summary>
        /// Получить заказ
        /// </summary>
        /// <param name="Id">ID заказа</param>
        /// <returns></returns>
        Order GetOrderById(int Id);

        /// <summary>
        /// Сформировать заказ
        /// </summary>
        /// <param name="OrderViewModel">Модель-предстваления заказа</param>
        /// <param name="CartViewModel">Модель-представления корзины</param>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns></returns>
        Order CreateNewOrder(OrderViewModel OrderViewModel, CartViewModel CartViewModel, string UserName);
    }
}
