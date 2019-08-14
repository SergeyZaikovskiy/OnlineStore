using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        ///  Вызов представления Shop (Набор товаров)
        /// </summary>
        /// <param name="SecID">ID секции для фильтра отбора товаров</param>
        /// <param name="CatID">ID категории для фильтра отбора товаров</param>
        /// <param name="Brands">Список брендов для фильтра отбора товаров из ViewComponent Brands</param>
        /// <param name="JsonBrands">Список брендов из ТагХелперов</param>
        /// <param name="MinP"></param>
        /// <param name="MaxP"></param>
        /// <param name="sortValue"></param>
        /// <returns></returns>
        public async Task<IActionResult> Shop(int? SecID, int? CatID, List<BrandViewModel> Brands, string JsonBrands, decimal? MinP, decimal? MaxP, string sortValue = SortEntityForProducts.NameAsc)
        {
            var brandsID = new List<int?>();            
            var catalog_model = new CatalogViewModel();
          
            //Определим откуда пришли данные, из тагхелпера сортировки или из Вьюкомпонента
            if ((Brands == null || Brands.Count == 0) && !String.IsNullOrEmpty(JsonBrands))
            {
                //распарсиваем Json в листинг брендов
                var BrandsFromJson = JsonConvert.DeserializeObject<List<BrandViewModel>>(JsonBrands);
                foreach (var br in BrandsFromJson)
                    if (br.Choosen)
                        brandsID.Add(br.Id);

                catalog_model.Brands = BrandsFromJson;                               
            }
            else
            {
                foreach (var br in Brands)
                    if (br.Choosen)
                        brandsID.Add(br.Id);

                catalog_model.Brands = Brands;

                if (Brands.Count > 0)
                {
                    if (sortValue == SortEntityForProducts.NameAsc) { sortValue = SortEntityForProducts.NameDes; }
                    else if (sortValue == SortEntityForProducts.NameDes) { sortValue = SortEntityForProducts.NameAsc; }
                    else if (sortValue == SortEntityForProducts.BrandAsc) { sortValue = SortEntityForProducts.BrandDes; }
                    else if (sortValue == SortEntityForProducts.BrandDes) { sortValue = SortEntityForProducts.BrandAsc; }
                    else if (sortValue == SortEntityForProducts.PriceAsc) { sortValue = SortEntityForProducts.PriceDes; }
                    else if (sortValue == SortEntityForProducts.PriceDes) { sortValue = SortEntityForProducts.PriceAsc; }
                }//Запуск из Вьюкомпонента Бренды, просто сохраняем текущую сортировку
            }                

            ProductFilter productFilter = new ProductFilter { SectionId = SecID, CategoryId = CatID, BrandIdCollection = brandsID, MinPrice = MinP, MaxPrice = MaxP  };

            //Получаем лист товаров по заданному фильтру
            var products =  _productData.GetProducts(productFilter);

            ////Для работы без пользовательского TagHelper, переключатель сортировок          
            //ViewData["NameSort"] = sortValue == SortEntityForProducts.NameAsc ? SortEntityForProducts.NameDes : SortEntityForProducts.NameAsc;           
            //ViewData["BrandSort"] = sortValue == SortEntityForProducts.BrandAsc ? SortEntityForProducts.BrandDes : SortEntityForProducts.BrandAsc;
            //ViewData["PriceSort"] = sortValue == SortEntityForProducts.PriceAsc ? SortEntityForProducts.PriceDes : SortEntityForProducts.PriceAsc;          
                

            //сортировка списка товаров
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

            
            //Заполняем оставшиеся данный для ViewModel с товарами
            catalog_model.SectionId = productFilter.SectionId;
            catalog_model.Products = products.Select(ProductViewModelMapper.CreateViewModel).ToList();                     

            if (productFilter.CategoryId is null)
            {
                catalog_model.CategoryId = _productData.GetCategories(productFilter).FirstOrDefault().id;
                catalog_model.Products = catalog_model.Products.Where(p => p.Category.id == catalog_model.CategoryId).ToList();
            }//если запрос идет только по секции, то принудительно выбираем все товары для первой попавшейся категории для данной секции
            else
            {
                catalog_model.CategoryId = productFilter.CategoryId;               
            }

            catalog_model.SortViewModel = new SortViewModelForProduct(sortValue);

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
