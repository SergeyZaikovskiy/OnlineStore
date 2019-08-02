using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Mappers
{
    /// <summary>
    /// Методы расширения для копирования данных из BrandViewModel в класс Brand и обратно
    /// </summary>
    public static class BrandViewModelMapper
    {
        public static void CopyTo(this BrandViewModel Model, Brand brand)
        {
            brand.id = Model.Id;
            brand.Name = Model.Name;
            brand.Order = Model.Order;
        }

        public static Brand CreateBrand(this BrandViewModel model)
        {
            var brand = new Brand();
            model.CopyTo(brand);
            return brand;
        }

        public static void CopyTo(this Brand brand, BrandViewModel model)
        {
            model.Id = brand.id;
            model.Name = brand.Name;
            model.Order = brand.Order;
            //model.ProductsCount = brand.Products.Count;
        }

        public static BrandViewModel CreateViewModel(this Brand brand)
        {
            var brandViewModel = new BrandViewModel();
            brand.CopyTo(brandViewModel);
            return brandViewModel;
        }
    }
}
