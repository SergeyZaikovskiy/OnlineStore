using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Interfeices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Components
{/// <summary>
 /// Компонент для области Категорий
 /// Вызывает компонент Category
 /// </summary>
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public async Task<IViewComponentResult> InvokeAsync()        {

            var categories = await Task.Run(() => _ProductData.GetCategories());
            return View(categories);
        }
    }
}
