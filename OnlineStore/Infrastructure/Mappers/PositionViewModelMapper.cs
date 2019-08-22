using OnlineStore.Areas.Admin.ViewModels;
using OnlineStore.Domain.Entities.EmployeesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Mappers
{
    /// <summary>
    /// Методы расширения для копирования данных из PositionViewModel в класс Position и обратно
    /// </summary>
    public static class PositionViewModelMapper
    {
        public static void CopyTo(this PositionViewModel Model, Position position)
        {
            position.id = Model.Id;
            position.Name = Model.Name;          
        }

        public static Position CreateBrand(this PositionViewModel model)
        {
            var position = new Position();
            model.CopyTo(position);
            return position;
        }

        public static void CopyTo(this Position position, PositionViewModel model)
        {
            model.Id = position.id;
            model.Name = position.Name;          
        }

        public static PositionViewModel CreateViewModel(this Position position)
        {
            var positionViewModel = new PositionViewModel();
            position.CopyTo(positionViewModel);
            return positionViewModel;
        }
    }
}
