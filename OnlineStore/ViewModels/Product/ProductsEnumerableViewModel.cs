using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.Product
{
    /// <summary>
    /// Модель представления товаров с возможностью сортировки
    /// </summary>
    public class ProductsEnumerableViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public SortViewModelForProduct SortViewModel { get; set; }
    }

}
