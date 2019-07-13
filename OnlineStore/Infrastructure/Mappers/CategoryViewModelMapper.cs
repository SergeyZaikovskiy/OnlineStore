using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Mappers
{
    /// <summary>
    /// Методы расширения для копирования данных из CategoryViewModel в класс Category и обратно
    /// </summary>
    public static class CategoryViewModelMapper
    {
        public static void CopyTo(this CategoryViewModel Model, Category category)
        {
            category.id = Model.Id;
            category.Name = Model.Name;
            category.Order = Model.Order;
        }

        public static Category CreateCategory(this CategoryViewModel model)
        {
            var category = new Category();
            model.CopyTo(category);
            return category;
        }

        public static void CopyTo(this Category category, CategoryViewModel model)
        {
            model.Id = category.id;
            model.Name = category.Name;
            model.Order = category.Order;            
        }

        public static CategoryViewModel CreateViewModel(this Category category)
        {
            var categoryViewModel = new CategoryViewModel();
            category.CopyTo(categoryViewModel);
            return categoryViewModel;
        }
    }
}
