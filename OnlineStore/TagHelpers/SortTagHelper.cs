using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.TagHelpers
{
    /// <summary>
 /// Тагхелпер для сортировки данных
 /// </summary>
    public class SortTagHelper : TagHelper
    {     

        //Значение текущего свойства
        public string Property { get; set; }
        //Значение активного свойства
        public string Current { get; set; }
        //Действие контроллера
        public string ActionName { get; set; }
        //Название контроллера
        public string ControllerName { get; set; }
        //Данные для передачи в контроллер
        public Dictionary<string, string> RouteData { get; set; }
        //Область контроллера
        public string AreaName { get; set; }
        //Нисходящая или восходящая сортировка
        public bool Up { get; set; }

        private IUrlHelperFactory urlHelperFactory;

        public SortTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {           

            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";

            var query = CheckDataRoute(RouteData);

            string url;
            if (AreaName==null)
                url = "/" + ControllerName + "/" + ActionName + "?" + query;
            else
                url = "/" + AreaName + "/" + ControllerName + "/" + ActionName + "?" + query;

           
            output.Attributes.SetAttribute("href", url);

            //Проверям свойство
            if (Current == Property)
            {
                TagBuilder tag = new TagBuilder("i");                
                tag.AddCssClass("glyphicon");

                if (Up == true)   // если сортировка по возрастанию
                    tag.AddCssClass("glyphicon-chevron-up");
                else   // если сортировка по убыванию
                    tag.AddCssClass("glyphicon-chevron-down");

                output.PreContent.AppendHtml(tag);
            }
        }

        private string CheckDataRoute(Dictionary<string, string> DataDictionary)
        {
            var fixedRouteValues = new Dictionary<string, string>();
            foreach (var (newKey, value) in DataDictionary.Where(r => !string.IsNullOrWhiteSpace(r.Value)))
            {
                var key = fixedRouteValues.Keys.FirstOrDefault(k => string.Equals(k, newKey, StringComparison.InvariantCultureIgnoreCase)) ?? newKey;
                fixedRouteValues[key] = value;
            }
            //Добавляем марршут в словарь
            fixedRouteValues.Add("sortValue", Property);

            var query = string.Join("&", fixedRouteValues.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            return query;
        }//проверка данных в словаре для машратизации и передачи данных в контроллер
    }

}
