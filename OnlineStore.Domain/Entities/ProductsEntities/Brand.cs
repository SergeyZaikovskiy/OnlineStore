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

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Section> Sections { get; set; } 

        // virtual - указание Entity Framework на то, что Products должно быть навигационным свойством!
        public  virtual ICollection<Product> Products { get; set; } 

        public Brand()
        {
            Categories = new List<Category>();
            Sections = new List<Section>();
            Products = new List<Product>();
        }

    }
}
