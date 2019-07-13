using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// Модель представления Категория товаров
    /// </summary>
    public class CategoryViewModel
    {
        public bool Choosen { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }
        public int ProductsCount { get; set; }

        List<BrandViewModel> Brands = new List<BrandViewModel>();
      
    
    }
}
