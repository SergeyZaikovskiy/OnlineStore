using OnlineStore.Domain.Entities.Base.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineStore.Domain.Entities.EmployeesEntities
{
    /// <summary>
    /// Класс сотрудника
    /// </summary>
    [Table("Employees")]
    public class Employee : BaseEntity
    {
        public string Name { get; set; }

        public string Patronimic { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public int PositionId { get; set; }

        [ForeignKey(nameof(PositionId))]
        public virtual Position Position { get; set; }

        public string Email { get; set; }
    }

}
