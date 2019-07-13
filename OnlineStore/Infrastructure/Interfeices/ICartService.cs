using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Interfeices
{
    /// <summary>
    /// Интерфейс для работы с корзиной
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Уменьшить кол-ва товара в корзине
        /// </summary>
        /// <param name="id">id товара</param>
        void DecrementCountItem(int id);

        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        /// <param name="id">id товара</param>
        void RemoveFromCart(int id);

        /// <summary>
        /// Удалить все
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// Добавить товар в корзину
        /// </summary>
        /// <param name="id">id товара</param>
        void AddToCart(int id);

        /// <summary>
        /// Сформировать корзину
        /// </summary>
        /// <returns></returns>
        CartViewModel TransformCart();

    }
}
