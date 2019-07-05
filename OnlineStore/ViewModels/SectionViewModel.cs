using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// Модель представления Секций товаров
    /// </summary>
    public class SectionViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int Order { get; set; }

        /// <summary>
        /// Вложенные Категории
        /// </summary>
        public List<CategoryViewModel> Categories = new List<CategoryViewModel>();

        /// <summary>
        /// Вложенные Бренды
        /// </summary>
        public List<BrandViewModel> Brands = new List<BrandViewModel>();       

    }
}
