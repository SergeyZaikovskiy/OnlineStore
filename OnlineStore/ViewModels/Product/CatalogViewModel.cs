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

        public List<BrandViewModel> Brands { get; set; }
       
        public List<ProductViewModel> Products { get; set; }

        public SortViewModelForProduct SortViewModel { get; set; }

        public CatalogViewModel() { }

        public CatalogViewModel(int? SecID, int? CatID, List<BrandViewModel> Brands, List<ProductViewModel> Products) {
            this.SectionId = SecID;
            this.Products = Products;
            this.CategoryId = CatID;
            this.Brands = Brands;     
        }

    }
}
