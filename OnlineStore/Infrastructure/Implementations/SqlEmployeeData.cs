using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.Domain.Entities.EmployeesEntities;
using OnlineStore.Infrastructure.Interfeices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Implementations
{
    /// <summary>
    /// Класс для работы с базой данный сотрудников
    /// </summary>
    public class SqlEmployeeData : IEmployee
    {
        private readonly OnlineStoreContext db;

        public SqlEmployeeData(OnlineStoreContext db) {
            this.db = db;
        }

        /// <summary>
        /// Добавить пользователя в базу
        /// </summary>
        /// <param name="emp"></param>
        public void AddEmployee(Employee emp)
        {
            if (emp is null) return;
            db.Employees.Add(emp);
            db.SaveChanges();
        }

        /// <summary>
        /// Получить всех сотрудников из базы по фильтру
        /// </summary>
        /// <returns></returns>
        public IQueryable<Employee> GetAllEmp(EmployeeFilter employeeFilter)
        {
            IQueryable<Employee> employee = db.Employees.Include(p=>p.Position);

            if (employeeFilter is null)
                return employee;

            if (!String.IsNullOrEmpty(employeeFilter.Name))
                employee = employee.Where(emp => emp.Name.Contains(employeeFilter.Name));

            if (!String.IsNullOrEmpty(employeeFilter.SurName))
                employee = employee.Where(emp => emp.Surname.Contains(employeeFilter.SurName));

            if (!String.IsNullOrEmpty(employeeFilter.Patronimic))
                employee = employee.Where(emp => emp.Patronimic.Contains(employeeFilter.Patronimic));

            if (employeeFilter.Positions != null && employeeFilter.Positions.Count>0)
                employee = employee.Where(emp => employeeFilter.Positions.Contains(emp.PositionId));         

            return employee;
        }

        /// <summary>
        /// Получить все должности из базы
        /// </summary>
        /// <returns></returns>
        public IQueryable<Position> GetAllPositions() => db.Positions.Include(pos=>pos.Employees);

        /// <summary>
        /// Получить сотрудника по ID из базы
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <returns></returns>
        public Employee GetById(int? id) => db.Employees.Include(e => e.Position)
            .FirstOrDefault(emp => emp.id == id);


        /// <summary>
        /// Получить должность по ID из базы
        /// </summary>
        /// <param name="id">ID должности</param>
        /// <returns></returns>
        public Position GetPositionById(int? id) => db.Positions.FirstOrDefault(pos => pos.id == id);

        /// <summary>
        /// Удалить сотрудника по ID из базы
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <returns></returns>
        public void RemoveEmployee(int? id)
        {
            var emp = db.Employees.FirstOrDefault(e => e.id == id);
            if (emp is null) return;

            db.Employees.Remove(emp);
            db.SaveChanges();
        }

        /// <summary>
        /// Обновить данные сотрудника по ID из базы
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <returns></returns>
        public void UpdateInfoEmployee(Employee emp)
        {
            if (emp.id > 0)
            {
                db.Employees.Update(emp);
            }
            db.SaveChanges();
        }
    }
}
