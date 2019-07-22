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
        /// <param name="_SectionId">Id секции товара</param>
        /// <param name="_BrandId">Id бренда товара</param>
        /// <returns></returns>
        public async Task<IActionResult> Shop(int? _SectionId, List<int?> _BrandIdCollection, int? _CategoryId)
        {
            var products = await _productData.GetProducts(new ProductFilter
            {
                SectionId = _SectionId,
                BrandIdCollection = _BrandIdCollection,
                CategoryId = _CategoryId
            }).AsNoTracking().ToListAsync();

            var catalog_model = new CatalogViewModel
            {
                BrandIdCollection = _BrandIdCollection,
                SectionId = _SectionId,
                CategoryId = _CategoryId,
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