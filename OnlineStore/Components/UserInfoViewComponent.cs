using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Components
{
    
    /// <summary>
     /// Компонент для области информации о пользователе
     /// Вызывает компонент UserInfo
     /// </summary>
    [ViewComponent(Name = "UserInfo")]
    public class UserInfoViewComponent : ViewComponent
    {
        /// <summary>
        /// Загрузка и отображения представления для Компонента UserInfo
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
                return View("UserInfoView");

            return View();
        }
    }
}
