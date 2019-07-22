using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// Модель представления для Логина пользователя
    /// </summary>
    public class LoginUserViewModel
    {
        [Display(Name = "Имя пользователя"), Required]
        [MaxLength(50, ErrorMessage = "Максимальная длина имени пользователя не дожна превышать 50 символов!")]
        public string UserName { get; set; }

        [Display(Name = "Пароль"), Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

    }
}
