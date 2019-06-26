using OnlineStore.Domain.Entities.Base.Classes;
using OnlineStore.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.ProductsEntities
{
    /// <summary>
    /// Класс для описания заказа пользователя
    /// </summary>
    public class Order : NamedEntity
    {        
        public virtual User User { get; set; }
        
        public string Phone { get; set; }
        
        public string Address { get; set; }
       
        public DateTime Date { get; set; }
       
        public  ICollection<OrderItem> OrderItems { get; set; }
    }    

}
