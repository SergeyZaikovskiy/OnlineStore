using OnlineStore.Domain.Entities.ProductsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Areas.Admin.ViewModels
{
    /// <summary>
    /// Модель для работы с изображениями товара
    /// </summary>
    public class ImagesCatalogViewModel
    {
        public int IdProduct { get; set; }
        public IEnumerable<FileModel> Images { get; set; }
    }
}
