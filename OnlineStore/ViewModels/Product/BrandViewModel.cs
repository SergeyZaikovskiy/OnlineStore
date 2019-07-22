using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// Модель представления Брендов
    /// </summary>
    public class BrandViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }
        public int ProductsCount { get; set; }
    }
}
