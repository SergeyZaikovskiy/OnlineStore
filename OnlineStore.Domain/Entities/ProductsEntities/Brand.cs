using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineStore.Domain.Entities.ProductsEntities
{
    /// <summary>
    /// Класс прендов товаров
    /// </summary>
    [Table("Brands")]
    public class Brand
    {
        public int Order { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public ICollection<Section> Sections { get; set; } = new List<Section>();

        // virtual - указание Entity Framework на то, что Products должно быть навигационным свойством!
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
