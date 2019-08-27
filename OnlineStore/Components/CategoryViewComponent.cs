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
{
 /// <summary>
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

        public async Task<IViewComponentResult> InvokeAsync(CatalogViewModel catalogViewModel)
        {

            var cats = await Task.Run(() => GetCategories(catalogViewModel));

            return View(cats);
        }

        public List<CategoryViewModel> GetCategories(CatalogViewModel catalogViewModel)
        {            
            var categories = _ProductData.GetCategories(catalogViewModel.productFilter);           

            var cats = categories.Select(cat => cat.CreateViewModel()).ToList();


            for (int i = 0; i < cats.Count; i++)
            {
                ProductFilter pf = new ProductFilter {SectionId= catalogViewModel.productFilter.SectionId, CategoryId= cats[i].Id };

                var countGoods = _ProductData.GetProducts(pf).Count();

                cats[i].ProductsCount = countGoods;

                if (cats[i].Id == catalogViewModel.productFilter.CategoryId) cats[i].Choosen = true;
                else cats[i].Choosen = false;
                cats[i].SectionID = (int)catalogViewModel.productFilter.SectionId;
            }

            return cats;
        }
    }
}
