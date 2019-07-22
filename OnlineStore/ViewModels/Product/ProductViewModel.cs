using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities.ProductsEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// Модель представления Товара
    /// </summary>
    public class ProductViewModel
    {
        [Display(Name = "ID"), HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Display(Name = "Название"), Required(ErrorMessage = "Поле обязательно для ввода!")]
        public string Name { get; set; }

        [Display(Name = "Заказ")]
        public int Order { get; set; }

        [Display(Name = "Секция")]
        public Section Section { get; set; }

        [Display(Name = "Категория")]
        public Category Category { get; set; }

        //public int SectionID { get; set;}

        [Display(Name = "Бренд")]
        public Brand Brand { get; set; }

        //public int BrandID { get; set; }

        [Display(Name = "Ссылка на фото"), HiddenInput(DisplayValue = false)]
        public FileModel Image { get; set; }

        [Display(Name = "Цена"), Required(ErrorMessage = "Поле обязательно для ввода!")]
        [RegularExpression(@"\d+(\,\d{1,2})?", ErrorMessage = "В поле дожно быть число, резделенное запятой")]
        public decimal Price { get; set; }

        [Display(Name = "Секции")]
        public IEnumerable<Section> Sections { get; set; }

        [Display(Name = "Бренды")]
        public IEnumerable<Brand> Brands { get; set; }

        [Display(Name = "Категории")]
        public IEnumerable<Category> Categories{ get; set; }

    }
}
