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
        public async Task<IActionResult> Shop(int? SecID, IEnumerable<BrandViewModel> brands, decimal? MinP, decimal? MaxP, int? CatID)
        {
            var brandsID = new List<int?>();

            foreach (var br in brands)
                if (br.Choosen)
                    brandsID.Add(br.Id);

            ProductFilter productFilter = new ProductFilter { SectionId = SecID, CategoryId = CatID, BrandIdCollection = brandsID, MinPrice = MinP, MaxPrice = MaxP };

            var products = await _productData.GetProducts(productFilter).AsNoTracking().ToListAsync();

            var catalog_model = new CatalogViewModel
            {
                BrandCollection = brands,
                SectionId = productFilter.SectionId,
                Products = products.Select(ProductViewModelMapper.CreateViewModel)
            };

            if (productFilter.CategoryId is null)
            {
                catalog_model.CategoryId = _productData.GetCategories(productFilter).FirstOrDefault().id;
                catalog_model.Products = catalog_model.Products.Where(p => p.Category.id == catalog_model.CategoryId);
            }
            else
            {
                catalog_model.CategoryId = productFilter.CategoryId;               
            }
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
