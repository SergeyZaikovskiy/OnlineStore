using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineStore.Areas.Admin.ViewModels;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Domain.SortsEntities;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.ViewModels;
using OnlineStore.ViewModels.Common;
using OnlineStore.ViewModels.Product;
using SmartBreadcrumbs.Attributes;

namespace OnlineStore.Areas.Admin.Controllers
{
    /// <summary>
    /// Контроллер для работы с товарами 
    ///  Позволяет просматривать, редактировать, загружать и удалять
    ///  </summary>
    [Area("Admin"), Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
    public class ProductListController : Controller
    {
        private readonly IProductData productData;

        public IHostingEnvironment appEnvironment;

        public ProductListController(IProductData productData, IHostingEnvironment appEnvironment)
        {
            this.productData = productData;
            this.appEnvironment = appEnvironment;
        }


        /// <summary>
        /// Страница с перечнем товаров
        /// </summary>
        /// <returns></returns>
        [Breadcrumb("Список товаров", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> Index(string Name, int[] Brands, string JsonBrands, int? SecID, int? CatID, decimal? MinP, decimal? MaxP,
            string sortValue = SortEntityForProducts.NameAsc, int page = 1, bool needChangeSort = true)
        {
            //Временный листинг ID выбранных брендов
            var brandsID = new List<int>();

            //Определим откуда пришли данные, из тагхелпера сортировки или из формы фильтра
            if ((Brands == null || Brands.Length == 0) && !String.IsNullOrEmpty(JsonBrands))
            {
                brandsID = JsonConvert.DeserializeObject<List<int>>(JsonBrands);
            } //распарсиваем Json в листинг брендов
            else { brandsID = Brands.ToList(); }

            //ФИЛЬМТРАЦИЯ ДАННЫХ
            //Получаем лист товаров по заданному фильтру
            ProductFilter productFilter = new ProductFilter { SectionId = SecID, CategoryId = CatID, BrandIdCollection = brandsID, MinPrice = MinP, MaxPrice = MaxP };

            if (productFilter.CategoryId is null)
            {
                productFilter.CategoryId = productData.GetCategories(productFilter).FirstOrDefault().id;
            }//если запрос идет только по секции, то принудительно выбираем все товары для первой попавшейся категории для данной секции           

            //Выборка товаров по фильтру
            var products = productData.GetProducts(productFilter);

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
        /// Подробный просмотр информации о товаре
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        public async Task<IActionResult> Details(int id)
        {
            //ProductViewModel productViewModel = new ProductViewModel();
            var product = await Task.Run(()=> productData.GetProductById(id));
            var productViewModel = product.CreateViewModel();

            if (productViewModel is null)
                return NotFound();

            return View(productViewModel);
        }

        /// <summary>
        /// Просмотр товара с вожможностью редактирования
        /// </summary>
        /// <param name="id">id товара</param>
        /// <returns></returns>
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        public async Task<IActionResult> Edit(int? idProduct, int? idImage)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            //было
            //ProductViewModel productViewModel;

            if (idProduct != null)
            {
                if (idProduct > 0)
                {
                    var product = await Task.Run(() => productData.GetProductById(idProduct));
                    productViewModel = product.CreateViewModel();
                    if (productViewModel is null)
                        return NotFound();
                }//товар уже есть в каталоге
                else
                {
                    //productViewModel = new ProductViewModel();
                    //productViewModel.id = 0;
                }//новый товар, с временным  ID=0, товар уже есть в представлении, но еще нет в базе

                if (idImage != null)
                {
                    //productViewModel.Image = productData.GetFileById(Convert.ToInt32(idImage));
                }//присвоить новое изображение
            }//уже существующий товар
            else
            {
                productViewModel = new ProductViewModel();
            }//новый товар


             //Пока передаем пустой как заглушку
            ProductFilter productFilter = new ProductFilter();


            productViewModel.Sections = await productData.GetSections().AsNoTracking().ToListAsync();//добавляем список секций


            productViewModel.Brands = await productData.GetBrands(productFilter).AsNoTracking().ToListAsync();//добавляем список брендов           


            productViewModel.Categories = await productData.GetCategories(productFilter).AsNoTracking().ToListAsync();//добавляем категории

            return View(productViewModel);
        }

        /// <summary>
        /// Отправка отредактированной формы товара в базу
        /// </summary>
        /// <param name="id">id товара</param>
        /// <returns></returns>
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        [HttpPost]
        public IActionResult Edit(ProductViewModel productView)
        {
            if (!ModelState.IsValid) return View(productView);

            var product = productView.CreateProduct();

            //Если ID у товара уже есть, то делаем изменения в базе, если ID нет, то добавляем в базу новый товар
            //if (productView.id > 0) { productData.UpdateInfoProduct(product); }
            //else { productData.AddProduct(product); }

            return RedirectToAction("Index");

        }

        /// <summary>
        /// Удаление товара из базы
        /// </summary>
        /// <param name="id">id товара</param>
        /// <returns></returns>
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            await Task.Run(()=> productData.RemoveProduct(id));
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Загрузка файлов на серевер через приложение
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        public async Task<IActionResult> UploadFiles(IFormFile Uploadedfile, int idProduct)
        {
            if (Uploadedfile != null)
            {
                string path = "/images/shop/" + Uploadedfile.FileName;

                using (var stream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await Uploadedfile.CopyToAsync(stream);
                }

                FileModel filemodel = new FileModel { Name = Uploadedfile.FileName, Path = path };
                productData.AddFileToBase(filemodel);
            }


            return RedirectToAction("ChangeImageForProduct", new { idProduct = idProduct });

            //return RedirectToAction("Index");
        }

        /// <summary>
        /// Получение каталога изображений
        /// </summary>
        /// <param name="IdProductd">Id товара</param>  
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        public async Task<IActionResult> ChangeImageForProduct(int IdProduct)
        {
            var imagesFiles = await productData.GetFiles().AsNoTracking().ToListAsync();          

            var imgViewModel = new ImagesCatalogViewModel { Images = imagesFiles, IdProduct = IdProduct };

            return View(imgViewModel);
        }

        ///// <summary>
        ///// Изменить изображение у товара
        ///// </summary>
        ///// <param name="IdImage">Номер изображения</param>
        /////  /// <param name="IdProduct">Номер товара</param>        
        //[Authorize(Roles = Domain.Entities.User.RoleAdmin)]
        //public IActionResult ConfirmChangeImageForProduct(int IdImage, int IdProduct )
        //{
        //    var product = productData.GetProductById(IdProduct);
        //    var productViewModel = product.CreateViewModel();
        //    productViewModel.Image = productData.GetFileById(IdImage);     

        //    return RedirectToAction("Edit", "ProductList", productViewModel);           
        //}


        private string SaveSort(string currentSortValue)
        {
            if (currentSortValue == SortEntityForProducts.NameAsc) { currentSortValue = SortEntityForProducts.NameDes; }
            else if (currentSortValue == SortEntityForProducts.NameDes) { currentSortValue = SortEntityForProducts.NameAsc; }
            else if (currentSortValue == SortEntityForProducts.BrandAsc) { currentSortValue = SortEntityForProducts.BrandDes; }
            else if (currentSortValue == SortEntityForProducts.BrandDes) { currentSortValue = SortEntityForProducts.BrandAsc; }
            else if (currentSortValue == SortEntityForProducts.CategoryAsc) { currentSortValue = SortEntityForProducts.CategoryDes; }
            else if (currentSortValue == SortEntityForProducts.CategoryDes) { currentSortValue = SortEntityForProducts.CategoryAsc; }
            else if (currentSortValue == SortEntityForProducts.SectionAsc) { currentSortValue = SortEntityForProducts.SectionDes; }
            else if (currentSortValue == SortEntityForProducts.SectionDes) { currentSortValue = SortEntityForProducts.SectionAsc; }
            else if (currentSortValue == SortEntityForProducts.PriceAsc) { currentSortValue = SortEntityForProducts.PriceDes; }
            else if (currentSortValue == SortEntityForProducts.PriceDes) { currentSortValue = SortEntityForProducts.PriceAsc; }

            return currentSortValue;
        }//Метод для сохранения текущей сортировки
    }

}