using OnlineStore.Domain.Entities.EmployeesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Interfeices
{
    /// <summary>
    /// Интерфейс для работы с сотрудниками
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Получаем всех сотрудников
        /// </summary>
        /// <returns></returns>
        IQueryable<Employee> GetAllEmp(EmployeeFilter employeeFilter);

        /// <summary>
        /// Получаем сотрудника по Id
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns></returns>
        Employee GetById(int? id);

        /// <summary>
        /// Добавляем сотрудника в базу
        /// </summary>
        /// <param name="emp">Сотрудника для удаления</param>
        void AddEmployee(Employee emp);

        /// <summary>
        /// Удаляем сотрудника из базы
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        void RemoveEmployee(int? id);

        /// <summary>
        /// Обновить информации или добавить нового сотрудника
        /// </summary>
        /// <param name="id">id сотрудника</param>
        void UpdateInfoEmployee(Employee emp);

        /// <summary>
        /// Получаем список должностей
        /// </summary>
        /// <returns></returns>
        IQueryable<Position> GetAllPositions();

        /// <summary>
        /// Должность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        Position GetPositionById(int? id);


    }
}
