using OnlineStore.Domain.Entities.Base.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineStore.Domain.Entities.ProductsEntities
{
    /// <summary>
    /// Класс элемента заказа
    /// </summary>
    public class OrderItem : BaseEntity
    {
        
        public virtual Order Order { get; set; }
        
        public virtual Product Product { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
       
        public int Quantity { get; set; }
    }
}
