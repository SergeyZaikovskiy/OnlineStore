using OnlineStore.Domain.Entities.EmployeesEntities;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Mappers
{
    public static class EmployeeViewModelMapper
    {
        /// <summary>
        /// Методы расширения для копирования данных из EmployeeViewModel в класс Employee и обратно
        /// </summary>
        public static void CopyTo(this EmployeeViewModel model, Employee emp)
        {
            //var pos = model.Positions.FirstOrDefault(p => p.Name == model.Position);

            emp.id = model.Id;
            emp.Name = model.Name;
            emp.Patronimic = model.Patronimic;
            emp.Surname = model.SurName;
            emp.Age = model.Age;
            emp.Email = model.Email;
            emp.PositionId = model.Position.id;

            //emp.PositionId = model.PositionId ;
        }

        public static Employee CreateEmployee(this EmployeeViewModel model)
        {
            var emp = new Employee();
            model.CopyTo(emp);
            return emp;
        }

        public static void CopyTo(this Employee emp, EmployeeViewModel model)
        {
            model.Id = emp.id;
            model.Name = emp.Name;
            model.Patronimic = emp.Patronimic;
            model.SurName = emp.Surname;
            model.Age = emp.Age;
            model.Email = emp.Email;
            model.Position = emp.Position;
            //model.PositionId = emp.PositionId;
        }

        public static EmployeeViewModel CreateViewModel(this Employee emp)
        {
            var employee_view_model = new EmployeeViewModel();
            emp.CopyTo(employee_view_model);

            return employee_view_model;
        }
    }
}
