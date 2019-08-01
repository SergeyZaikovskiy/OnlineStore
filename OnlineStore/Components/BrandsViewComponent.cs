using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.ViewModels;
using OnlineStore.ViewModels.Product;

namespace OnlineStore.Components
{
    /// <summary>
    /// Компонент для области брендов
    /// Вызывает компонент Brands
    /// </summary>
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData productDate)
        {
            _ProductData = productDate;
        }

        /// <summary>
        /// Загрузка и отображения представления для Компонента Brands
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(ProductFilter productFilter)
        {
            var brands =await Task.Run(()=> GetBrands(productFilter));

            var brandsEnumerable = new BrandsEnumerableViewModel {SectionID = productFilter.SectionId, CategoryID = productFilter.CategoryId, Brands=brands };

            return View(brandsEnumerable);
        }

        private IQueryable<BrandViewModel> GetBrands(ProductFilter productFilter)
        {
            var brands =  _ProductData.GetBrands(productFilter);                       

            var listOfBrands = brands.Select(brand => brand.CreateViewModel());

            return listOfBrands;
        }
    }
}
