using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// Детальное представление корзины
    /// </summary>
    public class CartDetailsViewModel
    {
        public CartViewModel CartViewModel { get; set; }

        public OrderViewModel OrderViewModel { get; set; }

        public CartDetailsViewModel() { }

        public CartDetailsViewModel(CartViewModel CartViewModel, OrderViewModel OrderViewModel) {
            this.OrderViewModel = OrderViewModel;
            this.CartViewModel = CartViewModel;
        }
    }
}
