using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Domain.SortsEntities;
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
        public async Task<IActionResult> Shop(int ? SecID, List<BrandViewModel> Brands, decimal? MinP, decimal? MaxP, int? CatID, int? sec, string sortValue = SortEntityForProducts.NameAsc)
        {
            var brandsID = new List<int?>();

            foreach (var br in Brands)
                if (br.Choosen)
                    brandsID.Add(br.Id);

            ProductFilter productFilter = new ProductFilter { SectionId = SecID, CategoryId = CatID, BrandIdCollection = brandsID, MinPrice = MinP, MaxPrice = MaxP };

            var products =  _productData.GetProducts(productFilter);

            //переключение сортировок
            switch (sortValue)
            {
                case SortEntityForProducts.NameDes:
                    products = products.OrderByDescending(s => s.Name);
                    break;

                case SortEntityForProducts.BrandAsc:
                    products = products.OrderBy(s => s.Brand);
                    break;
                case SortEntityForProducts.BrandDes:
                    products = products.OrderByDescending(s => s.Brand);
                    break;              

                case SortEntityForProducts.PriceAsc:
                    products = products.OrderBy(s => s.Price);
                    break;

                case SortEntityForProducts.PriceDes:
                    products = products.OrderByDescending(s => s.Price);
                    break;

                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            await products.AsNoTracking().ToListAsync();


            ProductsEnumerableViewModel productsEnumerableViewModel = new ProductsEnumerableViewModel { SortViewModel = new SortViewModelForProduct(sortValue), Products = products.Select(ProductViewModelMapper.CreateViewModel) };

            var catalog_model = new CatalogViewModel
            {
                BrandCollection = Brands,
                SectionId = productFilter.SectionId,
                ProductsWithSortModel = productsEnumerableViewModel
            };

            if (productFilter.CategoryId is null)
            {
                catalog_model.CategoryId = _productData.GetCategories(productFilter).FirstOrDefault().id;
                catalog_model.ProductsWithSortModel.Products = catalog_model.ProductsWithSortModel.Products.Where(p => p.Category.id == catalog_model.CategoryId);
            }//если запрос идет только по секции, то принудительно выбираем все товары для первой попавшейся категории для данной секции
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
