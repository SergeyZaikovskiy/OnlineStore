using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities.EmployeesEntities;
using OnlineStore.Domain.Entities.UserEntities;
using OnlineStore.Areas.Admin;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Domain.Enums;
using OnlineStore.ViewModels;
using OnlineStore.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Areas.Admin.ViewModels;

namespace OnlineStore.Areas.Admin.Controllers
{
    /// <summary>
    ///  Контроллер для работы со списком сотрубников
    ///  Позволяет просматривать, редактировать, загружать и удалять
    /// </summary>  
    [Area("Admin"), Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
    public class EmployeeController : Controller
    {
        private readonly IEmployee Employees;

        /// <summary>
        /// Конструктор Контроллера Employee
        /// </summary>
        /// <param name="employees"></param>
        public EmployeeController(IEmployee employees)
        {
            this.Employees = employees;
        }

        /// <summary>
        /// Вызов главного представления сотрудников
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(EnumSortForEmployee sortValue = EnumSortForEmployee.SurnameAsc)
        {
            var employees = await Employees.GetAllEmp().AsNoTracking().ToListAsync();

            //Для работы без пользовательского TagHelper
            //ViewData["SurnameSort"] = sortEmployee == SortEnum.SurnameAsc ? SortEnum.SurnameDes : SortEnum.SurnameAsc;
            //ViewData["NameSort"] = sortEmployee == SortEnum.NameAsc ? SortEnum.NameDes : SortEnum.NameAsc;
            //ViewData["PatronimicSort"] = sortEmployee == SortEnum.PatronimicAsc ? SortEnum.PatronimicDes : SortEnum.PatronimicAsc;
            //ViewData["AgeSort"] = sortEmployee == SortEnum.AgeAsc ? SortEnum.AgeDes : SortEnum.AgeAsc;
            //ViewData["PositionSort"] = sortEmployee == SortEnum.PosAsc ? SortEnum.PosDes : SortEnum.PosAsc;

           
            //переключение сортировок
            switch (sortValue)
            {
                case EnumSortForEmployee.SurnameDes:
                    employees = employees.OrderByDescending(s => s.Surname).ToList();
                    break;

                case EnumSortForEmployee.NameAsc:
                    employees = employees.OrderBy(s => s.Name).ToList();
                    break;
                case EnumSortForEmployee.NameDes:
                    employees = employees.OrderByDescending(s => s.Name).ToList();
                    break;

                case EnumSortForEmployee.PatronimicAsc:
                    employees = employees.OrderBy(s => s.Patronimic).ToList();
                    break;
                case EnumSortForEmployee.PatronimicDes:
                    employees = employees.OrderByDescending(s => s.Patronimic).ToList();
                    break;

                case EnumSortForEmployee.AgeAsc:
                    employees = employees.OrderBy(s => s.Age).ToList();
                    break;

                case EnumSortForEmployee.AgeDes:
                    employees = employees.OrderByDescending(s => s.Age).ToList();
                    break;

                case EnumSortForEmployee.PosAsc:
                    employees = employees.OrderBy(s => s.Position.Name).ToList();
                    break;
                case EnumSortForEmployee.PosDes:
                    employees = employees.OrderByDescending(s => s.Position.Name).ToList();
                    break;

                default:
                    employees = employees.OrderBy(s => s.Surname).ToList();
                    break;
            }

            //await employees.AsNoTracking().ToListAsync();

            //список сотрудников
            var employess_View_models = employees.Select(EmployeeViewModelMapper.CreateViewModel);
            //модель представления сотрудников с возможностью сортировки
            var employeeEnumerableView = new EmployeeEnumerableViewModel { employees = employess_View_models, SortViewModel = new SortViewModelForEmployees(sortValue) };

            return View(employeeEnumerableView);
        }

        /// <summary>
        /// Вызов представления для редактирования сотрудника Get-request
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns></returns>
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        public async Task<IActionResult> Edit(int? id)
        {
            EmployeeItemViewModel emp_view_model;

            if (id != null)
            {
                var emp = await Task.Run(()=>Employees.GetById(id));
                emp_view_model = emp.CreateViewModel();
                if (emp_view_model == null) return NotFound();
            }
            else
            {
                //если Сотрудник пустой (новый)
                emp_view_model = new EmployeeItemViewModel();
                emp_view_model.Position = new Position { id = 1 };
            }

            emp_view_model.Positions = await Employees.GetAllPositions().AsNoTracking().ToListAsync();

            return View(emp_view_model);
        }

        /// <summary>
        /// Отправка отредактированного сотрудника Post-request
        /// </summary>
        /// <param name="emp">Сотрудник</param>
        /// <param name="mapper">Маппер для автоматического заполнения данных в сотруднике из формы</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        public async Task<IActionResult> Edit(EmployeeItemViewModel emp_model)
        {
            if (!ModelState.IsValid) return View(emp_model);
            var emp = emp_model.CreateEmployee();

            if (emp_model.Id > 0) { await Task.Run(()=> Employees.UpdateInfoEmployee(emp)); }
            else { await Task.Run(() => Employees.AddEmployee(emp)); }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns></returns>
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            await Task.Run(()=>Employees.RemoveEmployee(id));
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Вызов представления с детализацией данных о сотруднике
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            EmployeeItemViewModel emp_view_model = new EmployeeItemViewModel();
            var emp = await Task.Run(() => Employees.GetById(id));
            emp_view_model = emp.CreateViewModel();
            if (emp_view_model == null) return NotFound();

            return View(emp_view_model);
        }
    }

}