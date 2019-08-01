using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.ViewModels;
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

        public CategoryViewComponent(IProductData productDate)
        {
            _ProductData = productDate;
        }

        public async Task<IViewComponentResult> InvokeAsync(ProductFilter productFilter)
        {

            var cats = await Task.Run(() => GetCategories(productFilter));

            return View(cats);
        }

        public List<CategoryViewModel> GetCategories(ProductFilter productFilter)
        {
            var categories = _ProductData.GetCategories(productFilter);

            var cats = categories.Select(cat => cat.CreateViewModel()).ToList();

            for (int i = 0; i < cats.Count; i++)
            {
                if (cats[i].Id == productFilter.CategoryId) cats[i].Choosen = true;
                else cats[i].Choosen = false;
                cats[i].SectionID = (int)productFilter.SectionId;
            }

            return cats;
        }
    }
}
