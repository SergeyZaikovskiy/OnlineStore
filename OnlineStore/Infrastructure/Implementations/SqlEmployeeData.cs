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


        public void AddEmployee(Employee emp)
        {
            if (emp is null) return;
            db.Employees.Add(emp);
            db.SaveChanges();
        }

        public IQueryable<Employee> GetAllEmp()=> db.Employees
                .Include(emp => emp.Position);       

        public IQueryable<Position> GetAllPositions() => db.Positions;       

        public Employee GetById(int? id) => db.Employees.Include(e => e.Position)
            .FirstOrDefault(emp => emp.id == id);
        

        public Position GetPositionById(int? id) => db.Positions.FirstOrDefault(pos => pos.id == id);


        public void RemoveEmployee(int? id)
        {
            var emp = db.Employees.FirstOrDefault(e => e.id == id);
            if (emp is null) return;

            db.Employees.Remove(emp);
            db.SaveChanges();
        }

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
