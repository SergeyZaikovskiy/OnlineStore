using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Mappers
{
    /// <summary>
    /// Методы расширения для копирования данных из ProductViewModel в класс Product и обратно
    /// </summary>
    public static class ProductViewModelMapper
    {
        public static void CopyTo(this ProductViewModel Model, Product product)
        {

            //Проверка на новый товар
            if (Model.Id != null && Model.Id != 0) { product.id = (int)Model.Id; }
            else { product.id = 0; }//создаем временный ID=0, до попаданя товара в базу

            product.Name = Model.Name;
            product.Order = Model.Order;
            product.FileId = Model.Image.id;
            product.Price = Model.Price;
            product.BrandId = Model.Brand.id;
            product.SectionId = Model.Section.id;
            product.CategoryId = Model.Category.id;
        }

        public static Product CreateProduct(this ProductViewModel model)
        {
            var product = new Product();
            model.CopyTo(product);
            return product;
        }

        public static void CopyTo(this Product product, ProductViewModel model)
        {
            model.Id = product.id;
            model.Name = product.Name;
            model.Order = product.Order;
            model.Image = product.FileModel;
            model.Price = product.Price;
            model.Brand = product.Brand;
            model.Section = product.Section;
            model.Category = product.Category;
        }

        public static ProductViewModel CreateViewModel(this Product product)
        {
            var productViewModel = new ProductViewModel();
            product.CopyTo(productViewModel);

            return productViewModel;
        }
    }
}
