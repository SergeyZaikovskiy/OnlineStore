using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Interfeices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Components
{
    public class CategoryTabViewComponent : ViewComponent
    {

        private readonly IProductData _ProductData;

        public CategoryTabViewComponent(IProductData ProductData)
        {
            _ProductData = ProductData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await Task.Run(() => _ProductData.GetBrands());
            return View();
        }
    }
}
