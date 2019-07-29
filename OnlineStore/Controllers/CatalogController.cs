using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.ViewModels;

namespace OnlineStore.Controllers
{
    /// <summary>
    /// Контроллер для представления Shop(набор товаров)
    /// </summary>
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData)
        {
            _productData = productData;
        }

        /// <summary>
        /// Вызов представления Shop (Набор товаров)
        /// </summary>
        /// <param name="productFilter">Фильтр товаров</param>
        /// <returns></returns>       
        public async Task<IActionResult> Shop(int? SecID, int? CatID, List<int?> BrIDCol, decimal? MinP, decimal? MaxP )
        {
        ProductFilter productFilter = new ProductFilter {SectionId = SecID, CategoryId=CatID, BrandIdCollection = BrIDCol, MinPrice = MinP, MaxPrice = MaxP};

        var products = await _productData.GetProducts(productFilter).AsNoTracking().ToListAsync();

            var catalog_model = new CatalogViewModel
            {
                BrandIdCollection = productFilter.BrandIdCollection,
                SectionId = productFilter.SectionId,
                CategoryId = productFilter.CategoryId,
                Products = products.Select(ProductViewModelMapper.CreateViewModel)
            };
            return View(catalog_model);
        }

        /// <summary>
        /// Вызов представления деталей товара
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await Task.Run(() => _productData.GetProductById(id));

            if (product is null)
                return NotFound();

            return View(product.CreateViewModel());

        }

    }
}