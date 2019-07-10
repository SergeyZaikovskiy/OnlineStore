using OnlineStore.Domain.Entities.Base.Classes;
using OnlineStore.Domain.Entities.ProductsEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.ServiceEntity
{
    /// <summary>
    /// Вспомогательный класс для определения многие ко многим класса Категорий к классу Брендов
    /// </summary>
    public class CategoryToBrand : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }    
      
    }
}
