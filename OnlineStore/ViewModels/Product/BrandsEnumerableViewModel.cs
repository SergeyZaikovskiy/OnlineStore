using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.Product
{
    /// <summary>
    /// Модель представления Брендов в виде списка с отсылкой у Категориям и Секциям
    /// </summary>
    public class BrandsEnumerableViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        public int? CategoryID { get; set; }

        public int? SectionID { get; set; }
    }
}
