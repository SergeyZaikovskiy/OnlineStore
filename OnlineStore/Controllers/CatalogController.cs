using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers
{
    /// <summary>
    /// Контроллер для работы с каталогом сайта
    /// </summary>
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {        
            
            return View();
        }
    }
}