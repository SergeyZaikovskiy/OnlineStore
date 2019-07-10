using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.ProductsEntities
{
    /// <summary>
    /// Класс для фильтрации товаров по параметрам
    /// </summary>
    public class ProductFilter
    {
        public int? SectionId { get; set; }

        public List<int?> BrandIdCollection { get; set; } = new List<int?>();

        public int? CategoryId { get; set; }

        public List<int> Identifocators { get; set; }
    }

}
