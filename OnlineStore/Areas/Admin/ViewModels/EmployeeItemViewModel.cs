using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities.EmployeesEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Areas.Admin.ViewModels
{   
    /// <summary>
    /// Модель представления одного сотрудника
    /// </summary>
    public class EmployeeItemViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя"), Required(ErrorMessage = "Поле обязательно для ввода!")]
        [MinLength(3, ErrorMessage = "Минимальная длина поля 3 символа")]
        public string Name { get; set; }

        [Display(Name = "Фамилия"), Required(ErrorMessage = "Поле обязательно для ввода!")]
        [MinLength(3, ErrorMessage = "Минимальная длина поля 3 символа")]
        public string SurName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronimic { get; set; }

        [Display(Name = "Возраст")]
        [RegularExpression(@"^[0-9]{0,2}", ErrorMessage = "В поле дожно быть число")]
        [Range(18, 90, ErrorMessage = "Возраст должен быть от 18 до 90 лет")]
        public int Age { get; set; }

        [Display(Name = "Электронный адрес"), DataType(DataType.EmailAddress, ErrorMessage = "Некорректный элеткронный адрес")]
        public string Email { get; set; }

        [Display(Name = "Должность")]
        public Position Position { get; set; }

        [Display(Name = "Должность")]
        public IEnumerable<Position> Positions { get; set; }


    }
}
