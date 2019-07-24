using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.Domain.Entities.ServiceEntity;

namespace OnlineStore.Components
{/// <summary>
 /// Компонент для области секции
 /// Вызывает компонент Sections
 /// </summary>
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public SectionsViewComponent(IProductData productDate)
        {
            _ProductData = productDate;
        }

        /// <summary>
        /// Загрузка и отображения представления для Компонента Sections
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sections = await Task.Run(()=>GetSections());

            return View(sections);            
        }

        private List<SectionViewModel> GetSections()
        {
            var sectionsFromDB = _ProductData.GetSections();

            List<SectionViewModel> sectionsToPassInViewModel = new List<SectionViewModel>();

            foreach (Section sec in sectionsFromDB)
            {
                var catToSec = _ProductData.GetCategoryByIdSection(sec.id);

                SectionViewModel sectionViewModel;

                if (catToSec.Count() > 0)
                {
                    List<Category> cat = new List<Category>();
                    foreach (SectionToCategory catSec in catToSec)
                    {
                        cat.Add(catSec.Category);
                    }
                    sectionViewModel = new SectionViewModel { Id = sec.id, Name = sec.Name, Order = sec.Order, Categories = cat.AsEnumerable(), CountNestedCategory = cat.Count };
                }
                else
                    sectionViewModel = new SectionViewModel { Id = sec.id, Name = sec.Name, Order = sec.Order, CountNestedCategory = 0 };

                sectionsToPassInViewModel.Add(sectionViewModel);
            }

            return sectionsToPassInViewModel;
        }

    }
}
