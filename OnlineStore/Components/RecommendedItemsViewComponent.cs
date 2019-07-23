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
    public class RecommendedItemsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public RecommendedItemsViewComponent(IProductData ProductData)
        {
            _ProductData = ProductData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var productViewModels = await Task.Run(() => GetProducts());

            return View(productViewModels.ToList());
        }

        private IQueryable<ProductViewModel> GetProducts()
        {
            ProductFilter productFilter = null;
            var products = _ProductData.GetProducts(productFilter, 9);           

            var listOfProducts = products.Select(product => product.CreateViewModel());

            return listOfProducts;
        }
    }
}
