using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.ViewModels;

namespace OnlineStore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee Employees;

        public EmployeeController(IEmployee Employees)
        {
            this.Employees = Employees;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await Employees.GetAllEmp().ToListAsync();
            EmployeesViewModel empModelView = new EmployeesViewModel();
            empModelView.EmployeesEnumerable = employees.AsEnumerable();
            return View(empModelView);
        }
    }
}