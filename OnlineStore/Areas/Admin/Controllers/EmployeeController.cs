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
using OnlineStore.ViewModels;
using OnlineStore.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Areas.Admin.ViewModels;
using OnlineStore.Domain.SortsEntities;
using OnlineStore.ViewModels.Common;
using SmartBreadcrumbs.Attributes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        /// <param name="sortValue">Название сортировки</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="needChangeSort">Нужно ли менять сортировку или просто сохранить текущую</param>
        /// <returns></returns>
        [Breadcrumb("Сотрудники", AreaName = "Admin", FromAction = "Index", FromController = typeof(HomeAdminController))]
        public async Task<IActionResult> Index(string name, string surname, string patronimic, List<int> positions, string jsonPositions,
             string sortValue = SortEntityForEmployee.SurnameAsc, int page = 1, bool needChangeSort = true)
        {

            //Временный листинг ID выбранных брендов
            var posList = new List<int>();

            //Определим откуда пришли данные, из тагхелпера сортировки или из формы
            if ((positions == null || positions.Count == 0) && !String.IsNullOrEmpty(jsonPositions))
            {
                posList = JsonConvert.DeserializeObject<List<int>>(jsonPositions);
            } //распарсиваем Json в листинг брендов
            else { posList = positions; }  

            var filter = new EmployeeFilter
            {
                Name = name, SurName = surname,
                Patronimic = patronimic,
                ChosenPositions = posList
            };

            var employees = Employees.GetAllEmp(filter);

            //Для работы без пользовательского TagHelper
            //ViewData["SurnameSort"] = sortEmployee == SortEnum.SurnameAsc ? SortEnum.SurnameDes : SortEnum.SurnameAsc;
            //ViewData["NameSort"] = sortEmployee == SortEnum.NameAsc ? SortEnum.NameDes : SortEnum.NameAsc;
            //ViewData["PatronimicSort"] = sortEmployee == SortEnum.PatronimicAsc ? SortEnum.PatronimicDes : SortEnum.PatronimicAsc;
            //ViewData["AgeSort"] = sortEmployee == SortEnum.AgeAsc ? SortEnum.AgeDes : SortEnum.AgeAsc;
            //ViewData["PositionSort"] = sortEmployee == SortEnum.PosAsc ? SortEnum.PosDes : SortEnum.PosAsc;


            //СОРТИРОВКА ДАННЫХ
            //сортировка списка товаров
            //Сохраним текущую сортировку если это необходимо
            if (!needChangeSort)
                sortValue = SaveSort(sortValue);

            switch (sortValue)
            {
                case SortEntityForEmployee.SurnameDes:
                    employees = employees.OrderByDescending(s => s.Surname);
                    break;

                case SortEntityForEmployee.NameAsc:
                    employees = employees.OrderBy(s => s.Name);
                    break;
                case SortEntityForEmployee.NameDes:
                    employees = employees.OrderByDescending(s => s.Name);
                    break;

                case SortEntityForEmployee.PatronimicAsc:
                    employees = employees.OrderBy(s => s.Patronimic);
                    break;
                case SortEntityForEmployee.PatronimicDes:
                    employees = employees.OrderByDescending(s => s.Patronimic);
                    break;

                case SortEntityForEmployee.AgeAsc:
                    employees = employees.OrderBy(s => s.Age);
                    break;

                case SortEntityForEmployee.AgeDes:
                    employees = employees.OrderByDescending(s => s.Age);
                    break;

                case SortEntityForEmployee.PosAsc:
                    employees = employees.OrderBy(s => s.Position.Name);
                    break;
                case SortEntityForEmployee.PosDes:
                    employees = employees.OrderByDescending(s => s.Position.Name);
                    break;

                default:
                    employees = employees.OrderBy(s => s.Surname);
                    break;
            }          


            //ПАГИНАЦИЯ ДАННЫХ
            //Пагинация
            int pageSize = 12;//размер страницы
            var count = await employees.CountAsync();//количество единиц товаров
            var PageEmployee = await employees.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();//количество страниц
            var Positions = await Task.Run(()=>Employees.GetAllPositions());


            var positionsViewModels = Positions.Select(PositionViewModelMapper.CreateViewModel).ToList();
            for (int i = 0; i < positionsViewModels.Count; i++)
            {
                if (posList.Contains(positionsViewModels[i].Id))
                    positionsViewModels[i].Choosen = true;
            }

            //модель представления сотрудников с возможностью сортировки
            var employeeEnumerableView = new EmployeeEnumerableViewModel
            { employees = PageEmployee.Select(EmployeeViewModelMapper.CreateViewModel),
                SortViewModel = new SortViewModelForEmployees(sortValue),
                PageViewModel = new PageViewModel(count, page, pageSize),
                PositionModels = positionsViewModels,
                employeeFilter = filter
            };

            return View(employeeEnumerableView);
        }

        /// <summary>
        /// Вызов представления для редактирования сотрудника Get-request
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns></returns>
        [Authorize(Roles = Domain.Entities.UserEntities.User.RoleAdmin)]
        [Breadcrumb("Редактировать", FromAction = "Index", FromController = typeof(HomeAdminController))]
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
                emp_view_model.Position = new Position { id=1};
            }

            var positions = await Employees.GetAllPositions().AsNoTracking().ToListAsync();
            emp_view_model.Positions = new SelectList(positions, "id", "Name", emp_view_model.Position.id);

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
        [Breadcrumb("Подробно", FromAction = "Index", FromController = typeof(HomeAdminController))]
        public async Task<IActionResult> Details(int id)
        {
            EmployeeItemViewModel emp_view_model = new EmployeeItemViewModel();
            var emp = await Task.Run(() => Employees.GetById(id));
            emp_view_model = emp.CreateViewModel();
            if (emp_view_model == null) return NotFound();

            return View(emp_view_model);
        }

        /// <summary>
        /// Метод сохранения сортировки
        /// </summary>
        /// <param name="currentSortValue"></param>
        /// <returns></returns>
        private string SaveSort(string currentSortValue)
        {
            if (currentSortValue == SortEntityForEmployee.NameAsc) { currentSortValue = SortEntityForEmployee.NameDes; }
            else if (currentSortValue == SortEntityForEmployee.NameDes) { currentSortValue = SortEntityForEmployee.NameAsc; }
            else if (currentSortValue == SortEntityForEmployee.SurnameAsc) { currentSortValue = SortEntityForEmployee.SurnameDes; }
            else if (currentSortValue == SortEntityForEmployee.SurnameDes) { currentSortValue = SortEntityForEmployee.SurnameAsc; }
            else if (currentSortValue == SortEntityForEmployee.PatronimicAsc) { currentSortValue = SortEntityForEmployee.PatronimicDes; }
            else if (currentSortValue == SortEntityForEmployee.PatronimicDes) { currentSortValue = SortEntityForEmployee.PatronimicAsc; }
            else if (currentSortValue == SortEntityForEmployee.AgeAsc) { currentSortValue = SortEntityForEmployee.AgeDes; }
            else if (currentSortValue == SortEntityForEmployee.AgeDes) { currentSortValue = SortEntityForEmployee.AgeAsc; }
            else if (currentSortValue == SortEntityForEmployee.PosAsc) { currentSortValue = SortEntityForEmployee.PosDes; }
            else if (currentSortValue == SortEntityForEmployee.PosDes) { currentSortValue = SortEntityForEmployee.PosAsc; }

            return currentSortValue;
        }//Метод для сохранения текущей сортировки
    }

}