﻿using Microsoft.AspNetCore.Mvc;
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

        public IViewComponentResult Invoke()
        {
            var productViewModels = GetProducts();         

            return View(productViewModels.ToList());
        }

        private IQueryable<ProductViewModel> GetProducts()
        {
            ProductFilter productFilter = null;
            var products = _ProductData.GetProducts(productFilter, 6);           

            var listOfProducts = products.Select(product => product.CreateViewModel());

            return listOfProducts;
        }
    }
}