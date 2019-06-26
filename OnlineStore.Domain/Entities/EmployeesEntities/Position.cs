using OnlineStore.Domain.Entities.Base.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineStore.Domain.Entities.EmployeesEntities
{
    /// <summary>
    /// Класс должность сотрудника
    /// </summary>
    [Table("Positions")]
    public class Position : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }

}
