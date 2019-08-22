using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.EmployeesEntities
{
    /// <summary>
    /// Класс для фильтрации сотрудников
    /// </summary>
    public class EmployeeFilter
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Patronimic { get; set; }
        public List<int>  Positions { get; set; }
    }
}
