using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IViewComponentResult Invoke()
        {
            var sections = GetSections();
            return View(sections);
        }



        private IQueryable<SectionViewModel> GetSections()
        {

            var sections = _ProductData.GetSections();

            //var parent_sections = sections.Where(section => section.ParentId == null)
            //    .Select(SectionViewModelMapper.CreateViewModel).ToList();


            //foreach (var par_section in parent_sections)
            //{
            //    var child_sections = sections.Where(section => section.ParentId == par_section.Id)
            //        .Select(SectionViewModelMapper.CreateViewModel);
            //    par_section.ChildrenSections.AddRange(child_sections);
            //    par_section.ChildrenSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            //}

            //parent_sections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            //var sections1 = parent_sections.AsQueryable();

            return sections;
        }

    }
}
