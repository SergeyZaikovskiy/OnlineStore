﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Breadcrumb("Список товаров", AreaName = "Admin", FromAction = "Index", FromController = typeof(HomeAdminController))]
        public async Task<IActionResult> Index(string Name, List<int> Brands, string JsonBrands, int? SecID, int? CatID, decimal? MinP, decimal? MaxP,
            string sortValue = SortEntityForProducts.NameAsc, int page = 1, bool needChangeSort = true)
        {
            //Временный листинг ID выбранных брендов
            var brandsID = new List<int>();

            //Определим откуда пришли данные, из тагхелпера сортировки или из формы фильтра
            if ((Brands == null || Brands.Count == 0) && !String.IsNullOrEmpty(JsonBrands))
            {
                brandsID = JsonConvert.DeserializeObject<List<int>>(JsonBrands);
            } //распарсиваем Json в листинг брендов
            else { brandsID = Brands; }

            //ФИЛЬМТРАЦИЯ ДАННЫХ
            //Получаем лист товаров по заданному фильтру
            ProductFilter productFilter = new ProductFilter {Name =Name, SectionId = SecID, CategoryId = CatID, BrandIdCollection = brandsID, MinPrice = MinP, MaxPrice = MaxP };
                              

            //Выборка товаров по фильтру
            var products = productData.GetProducts(productFilter);

         
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


                case SortEntityForProducts.SectionAsc:
                    products = products.OrderBy(s => s.Brand);
                    break;

                case SortEntityForProducts.SectionDes:
                    products = products.OrderByDescending(s => s.Brand);
                    break;

                case SortEntityForProducts.CategoryAsc:
                    products = products.OrderBy(s => s.Brand);
                    break;

                case SortEntityForProducts.CategoryDes:
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
            int pageSize = 12;//размер страницы
            var count = await products.CountAsync();//количество единиц товаров
            var PageProducts = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();//количество страниц


            var sections = await productData.GetSections().ToListAsync();          
            sections.Insert(0, new Section { Name = "Все", id = 0 });
            var categories = await productData.GetCategories(null).ToListAsync();
            categories.Insert(0, new Category { Name = "Все", id = 0 });

            var brands = await productData.GetBrands(null).ToListAsync();
            var BrandsViewModels = brands.Select(brand => brand.CreateViewModel()).ToList();
            for (int i = 0; i < BrandsViewModels.Count; i++)
            {
                if (productFilter.BrandIdCollection.Contains(BrandsViewModels[i].Id))
                    BrandsViewModels[i].Choosen = true;
            }

            //Заполняем данныe для ViewModel с товарами
            var productListViewModel = new ProductListViewModel
            {
                productFilter = productFilter,               
                Products = PageProducts.Select(ProductViewModelMapper.CreateViewModel).ToList(),              
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModelForProduct(sortValue),
                Sections = new SelectList(sections, "id", "Name"),
                Categories = new SelectList(categories, "id", "Name"),
                Brands = BrandsViewModels
            };

            return View(productListViewModel);
        }

        /// <summary>
        /// Подробный просмотр информации о товаре
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        [Breadcrumb("Подробно", FromAction = "Index", FromController = typeof(HomeAdminController))]
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
        [Breadcrumb("Редактировать", FromAction = "Index", FromController = typeof(HomeAdminController))]
        public async Task<IActionResult> Edit(int? idProduct, int? idImage)
        {
           
            //emp_view_model.Positions = new SelectList(positions, "id", "Name", emp_view_model.Position.id);

            ProductViewModel productViewModel;
           
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
                    productViewModel = new ProductViewModel();
                    productViewModel.Id = 0;
                }//новый товар, с временным  ID=0, товар уже есть в представлении, но еще нет в базе

                if (idImage != null)
                {
                    productViewModel.Image = productData.GetFileById(Convert.ToInt32(idImage));
                }//присвоить новое изображение
            }//уже существующий товар
            else
            {
                productViewModel = new ProductViewModel();
            }//новый товар

            var sections = await productData.GetSections().ToListAsync();
            var selectedSection = productViewModel.Section != null ? productViewModel.Section.id : 1;

            var categories = await productData.GetCategories(null).ToListAsync();
            var selectedCategory = productViewModel.Category != null ? productViewModel.Category.id : 1;

            var brands = await productData.GetBrands(null).ToListAsync();
            var selectedBrand = productViewModel.Brand != null ? productViewModel.Brand.id : 1;


            productViewModel.Sections = new SelectList(sections, "id", "Name", selectedSection);
            productViewModel.Categories = new SelectList(categories, "id", "Name", selectedCategory);
            productViewModel.Brands = new SelectList(brands, "id", "Name", selectedBrand);

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
            productData.UpdateInfoOrAddProduct(product);           
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


         /// <summary>
         /// Метод сохранения сортировки
         /// </summary>
         /// <param name="currentSortValue"></param>
         /// <returns></returns>
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