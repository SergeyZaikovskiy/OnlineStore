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
            var sectionsFromDB = await _ProductData.GetSections().ToListAsync();
            List<SectionViewModel> sectionsToPassInViewModel = new List<SectionViewModel>();

            foreach (OnlineStore.Domain.Entities.ProductsEntities.Section sec in sectionsFromDB)
            {
                var catToSec = await _ProductData.GetCategoryByIdSection(sec.id).ToListAsync();
                SectionViewModel sectionViewModel;
                if (catToSec.Count > 0)
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
            return View(sectionsToPassInViewModel.AsEnumerable());
        }



        //private IQueryable<SectionViewModel> GetSections()
        //{

        //    //var sections = _ProductData.GetSections();

        //    //var parent_sections = sections.Where(section => section.ParentId == null)
        //    //    .Select(SectionViewModelMapper.CreateViewModel).ToList();


        //    //foreach (var par_section in parent_sections)
        //    //{
        //    //    var child_sections = sections.Where(section => section.ParentId == par_section.Id)
        //    //        .Select(SectionViewModelMapper.CreateViewModel);
        //    //    par_section.ChildrenSections.AddRange(child_sections);
        //    //    par_section.ChildrenSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
        //    //}

        //    //parent_sections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
        //    //var sections1 = parent_sections.AsQueryable();

        //    return sections;
        //}

    }
}
