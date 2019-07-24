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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands =await Task.Run(()=> GetBrands());
            
            return View(brands);
        }

        private IQueryable<BrandViewModel> GetBrands()
        {
            var brands =  _ProductData.GetBrands();

            ///ProductFilter filter = null;

            //var products = _ProductData.GetProducts(filter);

            var listOfBrands = brands.Select(brand => brand.CreateViewModel(0));

            return listOfBrands;
        }
    }
}
