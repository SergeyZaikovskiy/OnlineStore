using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.ViewModels;
using OnlineStore.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Components
{
    public class CategoryTabViewComponent : ViewComponent
    {

        private readonly IProductData _ProductData;

        public CategoryTabViewComponent(IProductData ProductData)
        {
            _ProductData = ProductData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryViewModels = await Task.Run(() => GetModels());
            return View(categoryViewModels);
        }


        private List<CategoryTabViewModel> GetModels()
        {
            List<CategoryTabViewModel> categoryViewModels = new List<CategoryTabViewModel>();

            var sections = _ProductData.GetSections();         
                        
            foreach(var sec in sections)
            {
                

                ProductFilter productFilter = new ProductFilter { SectionId = sec.id};

                IEnumerable<ProductViewModel> productViewModel = _ProductData.GetProducts(productFilter, 4)
                    .Select(product => product.CreateViewModel())
                    .AsEnumerable();

                var sectionViewModel = sec.CreateViewModel();

                CategoryTabViewModel categoryTabViewModel = new CategoryTabViewModel {Products = productViewModel , Section = sectionViewModel };

                categoryViewModels.Add(categoryTabViewModel);
            }          
            
            return categoryViewModels;
        }
    }
}
