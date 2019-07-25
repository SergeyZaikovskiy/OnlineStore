using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.Product
{
    /// <summary>
    /// View модель для компонента CategoryTab
    /// </summary>
    public class CategoryTabViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public SectionViewModel Section { get; set; }
    }
}
