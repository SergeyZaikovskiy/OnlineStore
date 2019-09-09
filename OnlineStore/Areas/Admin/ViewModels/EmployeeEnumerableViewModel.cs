using OnlineStore.Domain.Entities.EmployeesEntities;
using OnlineStore.ViewModels;
using OnlineStore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Areas.Admin.ViewModels
{
    
    /// <summary>
 /// Модель представления сотрудников с сортировкой, пагинацией и фильтром
 /// </summary>
    public class EmployeeEnumerableViewModel
    {
        public IEnumerable<EmployeeItemViewModel> employees { get; set; }

        public List<PositionViewModel> PositionModels { get; set; }
        public SortViewModelForEmployees SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public EmployeeFilter employeeFilter { get; set; }

    }
}
