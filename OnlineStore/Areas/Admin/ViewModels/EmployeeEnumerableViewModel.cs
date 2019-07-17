using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Areas.Admin.ViewModels
{
    
    /// <summary>
 /// Модель представления сотрудников с сортировкой
 /// </summary>

    public class EmployeeEnumerableViewModel
    {
        public IEnumerable<EmployeeItemViewModel> employees { get; set; }
        public SortViewModelForEmployees SortViewModel { get; set; }

    }
}
