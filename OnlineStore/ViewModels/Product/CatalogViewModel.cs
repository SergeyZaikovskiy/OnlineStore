using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.ViewModels.Common;
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

        public ProductFilter productFilter { get; set; }

        public List<ProductViewModel> Products { get; set; }

        public SortViewModelForProduct SortViewModel { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public CatalogViewModel() { }

        public CatalogViewModel(int? SecID, int? CatID, List<int> Brands, List<ProductViewModel> Products) {

            productFilter = new ProductFilter
            {
                SectionId = SecID,
                CategoryId = CatID,
                BrandIdCollection = Brands
            };
             this.Products = Products;                
        }

    }
}
