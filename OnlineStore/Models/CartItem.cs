using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    /// <summary>
    /// Модель данных единицы товара в корзине
    /// </summary>
    public class CartItem
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }

}
