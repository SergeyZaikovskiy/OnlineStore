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
using OnlineStore.Areas.Admin.ViewModels;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Domain.Enums;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.ViewModels;

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
        public async Task<IActionResult> Index(EnumSortForProducts sortValue = EnumSortForProducts.NameAsc)
        {
            var productList = productData.GetProducts(new Domain.Entities.ProductsEntities.ProductFilter());

            //переключение сортировок
            switch (sortValue)
            {
                case EnumSortForProducts.NameDes:
                    productList = productList.OrderByDescending(s => s.Name);
                    break;

                case EnumSortForProducts.BrandAsc:
                    productList = productList.OrderBy(s => s.Brand);
                    break;
                case EnumSortForProducts.BrandDes:
                    productList = productList.OrderByDescending(s => s.Brand);
                    break;

                case EnumSortForProducts.SectionAsc:
                    productList = productList.OrderBy(s => s.Section);
                    break;
                case EnumSortForProducts.SectionDes:
                    productList = productList.OrderByDescending(s => s.Section);
                    break;

                case EnumSortForProducts.PriceAsc:
                    productList = productList.OrderBy(s => s.Price);
                    break;

                case EnumSortForProducts.PriceDes:
                    productList = productList.OrderByDescending(s => s.Price);
                    break;

                default:
                    productList = productList.OrderBy(s => s.Name);
                    break;
            }

            await productList.AsNoTracking().ToListAsync();

            //список товаров
            var products_view_model = productList.Select(ProductViewModelMapper.CreateViewModel);
            //модель представиления списка с возможностью сортировки
            var productsEnumerableView = new ProductsEnumerableViewModel { products = products_view_model, SortViewModel = new SortViewModelForProduct(sortValue) };

            return View(productsEnumerableView);
        }

        /// <summary>
        /// Подробный просмотр информации о товаре
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        public IActionResult Details(int id)
        {
            //ProductViewModel productViewModel = new ProductViewModel();
            var product = productData.GetProductById(id);
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
        public IActionResult Edit(int? idProduct, int? idImage)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            //было
            //ProductViewModel productViewModel;

            if (idProduct != null)
            {
                if (idProduct > 0)
                {
                    var product = productData.GetProductById(idProduct);
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

            productViewModel.Sections = productData.GetSections();//добавляем список секций
            productViewModel.Brands = productData.GetBrands();//добавляем список брендов

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
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            productData.RemoveProduct(id);
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
            var imagesFiles = productData.GetFiles();
            await imagesFiles.AsNoTracking().ToListAsync();

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
    }

}