using OnlineStore.Domain.Entities.Base.Classes;
using OnlineStore.Domain.Entities.ProductsEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.ServiceEntity
{
    /// <summary>
    /// Вспомогательный класс для определения многие ко многим класса Секций к классу Категорий
    /// </summary>
    public class SectionToCategory:BaseEntity
    {
        public int SectionId { get; set; }
        public Section Section { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
