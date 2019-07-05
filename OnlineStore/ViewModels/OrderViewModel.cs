using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// Модель представления для формы Заказа
    /// </summary>
    public class OrderViewModel
    {
        [Display(Name = "Пользователь"), Required]
        public string Name { get; set; }

        [Display(Name = "Телефон"), Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Адрес"), Required]
        public string Address { get; set; }

    }
}
