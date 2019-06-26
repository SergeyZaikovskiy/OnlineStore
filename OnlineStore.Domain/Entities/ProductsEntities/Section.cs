using OnlineStore.Domain.Entities.Base.Classes;
using OnlineStore.Domain.Entities.Base.Interfeices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineStore.Domain.Entities.ProductsEntities
{
    /// <summary>
    /// Класс секций товаров
    /// </summary>
    [Table("Sections")]
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
       
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public ICollection<Brand> Brands { get; set; } = new List<Brand>();

        // virtual - указание Entity Framework на то, что Products должно быть навигационным свойством!
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
