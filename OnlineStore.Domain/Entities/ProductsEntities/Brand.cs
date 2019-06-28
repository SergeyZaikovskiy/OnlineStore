using OnlineStore.Domain.Entities.Base.Classes;
using OnlineStore.Domain.Entities.Base.Interfeices;
using OnlineStore.Domain.Entities.ServiceEntity;
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
    public class Brand : NamedEntity, IOrderedEntity

    {
        public int Order { get; set; }

        public virtual ICollection<CategoryToBrand> BrandToCat { get; set; } = new List<CategoryToBrand>();
        // virtual - указание Entity Framework на то, что Products должно быть навигационным свойством!
        public  virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
