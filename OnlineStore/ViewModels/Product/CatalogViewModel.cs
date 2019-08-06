using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// Модель представления каталога товаров
    /// </summary>
    public class CatalogViewModel
    {
        public int? SectionId { get; set; }

        public int? CategoryId { get; set; }        

        public IEnumerable<BrandViewModel> BrandCollection { get; set; } 

        public ProductsEnumerableViewModel ProductsWithSortModel { get; set; }

    }
}
