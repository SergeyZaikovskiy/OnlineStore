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
using OnlineStore.ViewModels.Common;
using SmartBreadcrumbs.Attributes;

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
        /// <param name="Brands">Массив брендов для фильтра из ViewComponent Brand</param>
        /// <param name="JsonBrands">Список брендов для фильтра из Тагхелперов</param>
        /// <param name="SecID">ID секции для фильтра отбора товаров</param>
        /// <param name="CatID">ID категории для фильтра отбора товаров</param>
        /// <param name="MinP">Минимальная цена товара для фильтра</param>
        /// <param name="MaxP">Максимальная цена товара для фильтра</param>
        /// <param name="sortValue">Название сортировки товаров</param>
        /// <param name="page">Номер текущей страницы для пагинации</param>
        /// <param name="needChangeSort">Нужно ли менять сортировку или сохранить текущую</param>
        /// <returns></returns>       
        [Breadcrumb("Каталог", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> Shop(int[] Brands, string JsonBrands, int? SecID, int? CatID,  decimal? MinP, decimal? MaxP, 
            string sortValue = SortEntityForProducts.NameAsc, int page = 1, bool needChangeSort = true)
        {
            //Временный листинг ID выбранных брендов
            var brandsID = new List<int>();           
            
            //Определим откуда пришли данные, из тагхелпера сортировки или из Вьюкомпонента
            if ((Brands == null || Brands.Length == 0) && !String.IsNullOrEmpty(JsonBrands)) {
                brandsID = JsonConvert.DeserializeObject<List<int>>(JsonBrands);} //распарсиваем Json в листинг брендов
            else {brandsID = Brands.ToList(); }

            //ФИЛЬМТРАЦИЯ ДАННЫХ
            //Получаем лист товаров по заданному фильтру
            ProductFilter productFilter = new ProductFilter { SectionId = SecID, CategoryId = CatID, BrandIdCollection = brandsID, MinPrice = MinP, MaxPrice = MaxP  };

            if (productFilter.CategoryId is null)
            {             
                productFilter.CategoryId = _productData.GetCategories(productFilter).FirstOrDefault().id;
            }//если запрос идет только по секции, то принудительно выбираем все товары для первой попавшейся категории для данной секции           

            //Выборка товаров по фильтру
            var products = _productData.GetProducts(productFilter);

            ////Для работы без пользовательского TagHelper, переключатель сортировок          
            //ViewData["NameSort"] = sortValue == SortEntityForProducts.NameAsc ? SortEntityForProducts.NameDes : SortEntityForProducts.NameAsc;           
            //ViewData["BrandSort"] = sortValue == SortEntityForProducts.BrandAsc ? SortEntityForProducts.BrandDes : SortEntityForProducts.BrandAsc;
            //ViewData["PriceSort"] = sortValue == SortEntityForProducts.PriceAsc ? SortEntityForProducts.PriceDes : SortEntityForProducts.PriceAsc;          

            //СОРТИРОВКА ДАННЫХ
            //сортировка списка товаров
            //Сохраним текущую сортировку если это необходимо
            if (!needChangeSort)
                sortValue = SaveSort(sortValue);

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

            //ПАГИНАЦИЯ ДАННЫХ
            //Пагинация
            int pageSize = 6;//размер страницы
            var count = await products.CountAsync();//количество единиц товаров
            var PageProducts = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();//количество страниц

            //Заполняем данныe для ViewModel с товарами
            var catalog_model = new CatalogViewModel
            {
                SectionId = productFilter.SectionId,
                CategoryId = productFilter.CategoryId,
                Products = PageProducts.Select(ProductViewModelMapper.CreateViewModel).ToList(),
                Brands = brandsID,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModelForProduct(sortValue)
             };         

            return View(catalog_model);
        }

        /// <summary>
        /// Вызов представления деталей товара
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Breadcrumb("Детали товара", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await Task.Run(() => _productData.GetProductById(id));

            if (product is null)
                return NotFound();

            return View(product.CreateViewModel());
        }

        private string SaveSort(string currentSortValue)
        {
            if (currentSortValue == SortEntityForProducts.NameAsc) { currentSortValue = SortEntityForProducts.NameDes; }
            else if (currentSortValue == SortEntityForProducts.NameDes) { currentSortValue = SortEntityForProducts.NameAsc; }
            else if (currentSortValue == SortEntityForProducts.BrandAsc) { currentSortValue = SortEntityForProducts.BrandDes; }
            else if (currentSortValue == SortEntityForProducts.BrandDes) { currentSortValue = SortEntityForProducts.BrandAsc; }
            else if (currentSortValue == SortEntityForProducts.PriceAsc) { currentSortValue = SortEntityForProducts.PriceDes; }
            else if (currentSortValue == SortEntityForProducts.PriceDes) { currentSortValue = SortEntityForProducts.PriceAsc; }

            return currentSortValue;
        }//Метод для сохранения текущей сортировки
    }

}
