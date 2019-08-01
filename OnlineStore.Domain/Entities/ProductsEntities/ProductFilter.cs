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

        public IEnumerable<int?> BrandIdCollection { get; set; }

        public int? CategoryId { get; set; }

        public decimal? MinPrice { get;set; }

        public decimal? MaxPrice { get; set; }

        public List<int> Identifocators { get; set; }
    }

}
