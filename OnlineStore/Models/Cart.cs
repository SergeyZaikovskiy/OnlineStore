using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    /// <summary>
    /// Модельданный для корзины
    /// </summary>
    public class Cart
    {
        public List<CartItem> Items = new List<CartItem>();

        public int CartCount => Items?.Sum(item => item.Quantity) ?? 0;
    }

}
