using OnlineStore.Domain.Entities.ProductsEntities;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Mappers
{
    /// <summary>
    /// Методы расширения для копирования данных из SectionViewModel в класс Section и обратно
    /// </summary>
    public static class SectionViewModelMapper
    {
        public static void CopyTo(this SectionViewModel Model, Section section)
        {
            section.id = Model.Id;
            section.Name = Model.Name;
            section.Order = Model.Order;
        }

        public static Section CreateSection(this SectionViewModel model)
        {
            var section = new Section();
            model.CopyTo(section);
            return section;
        }

        public static void CopyTo(this Section section, SectionViewModel model)
        {
            model.Id = section.id;
            model.Name = section.Name;
            model.Order = section.Order;            
        }

        public static SectionViewModel CreateViewModel(this Section section)
        {
            var sectionViewModel = new SectionViewModel();
            section.CopyTo(sectionViewModel);
            return sectionViewModel;
        }
    }
}
