using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.ViewModels;
using OnlineStore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Areas.Admin.ViewModels
{
    public class ProductListViewModel
    {
        public ProductFilter productFilter { get; set; }
        public SelectList Sections { get; set; }        

        public SelectList Categories { get; set; }

        public List<BrandViewModel> Brands{ get; set; }

        public List<ProductViewModel> Products { get; set; }

        public SortViewModelForProduct SortViewModel { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public ProductListViewModel() { }

       
    }
}
