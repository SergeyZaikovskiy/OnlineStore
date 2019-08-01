using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.Infrastructure.Interfeices;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Components
{

    /// <summary>
    /// Компонент для области брендов
    /// Вызывает компонент Section
    /// </summary>
    public class SectionViewComponent: ViewComponent
    {
        private readonly IProductData _ProductData;

        public SectionViewComponent(IProductData productDate)
        {
            _ProductData = productDate;
        }

        /// <summary>
        /// Загрузка и отображения представления для Компонента Section
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(ProductFilter productFilter)
        {
            var sections = await Task.Run(() => GetSections(productFilter));

            return View(sections);
        }

        private List<SectionViewModel> GetSections(ProductFilter productFilter)
        {
            var sections = _ProductData.GetSections().Select(s => s.CreateViewModel()).ToList();          

            for (int i = 0; i < sections.Count; i++)
            {
                if (sections[i].Id == productFilter.SectionId) sections[i].Choosen = true;
                else sections[i].Choosen = false;                
            }
            return sections;
        }
    }
}

