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

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public List<int> Identifocators { get; set; }
    }

}
