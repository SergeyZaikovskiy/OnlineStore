using OnlineStore.Domain.Entities.EmployeesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    public class EmployeesViewModel
    {
        public IEnumerable<Employee> EmployeesEnumerable { get; set; }

        public IEnumerable<Position> PositionsEnumerable { get; set; }
    }
}
