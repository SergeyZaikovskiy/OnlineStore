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
{/// <summary>
 /// Хелпер для сортировки
 /// </summary>
    public class SortTagHelper : TagHelper
    {
        //public int? SectionId { get; set; }

        //public int? CategoryId { get; set; }

        //public List<BrandViewModel> BrandsList { get; set; }

        //Значение текущего свойства
        public string Property { get; set; }
        //Значение активного свойства
        public string Current { get; set; }
        //Действие контроллера
        public string ActionName { get; set; }
        //Название контроллера
        public string ControllerName { get; set; }
        //данные для передачи в контроллер
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
            var fixedRouteValues = new Dictionary<string, string>();
            foreach (var (newKey, value) in RouteData.Where(r => !string.IsNullOrWhiteSpace(r.Value)))
            {
                var key = fixedRouteValues.Keys.FirstOrDefault(k => string.Equals(k, newKey, StringComparison.InvariantCultureIgnoreCase)) ?? newKey;
                fixedRouteValues[key] = value;
            }
            fixedRouteValues.Add("sortValue", Property);

            var query = string.Join("&", fixedRouteValues.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";

            // /Catalog/Shop?secid=1&catid=6
            //string url = urlHelper.Action(ActionName, ControllerName, query);
            if(AreaName==null)
                string url = "/" + ControllerName + "/" + ActionName + "?" + query;
            else

            //string url = urlHelper.Action(ActionName, ControllerName,  new { sortValue = Property, Area = AreaName });
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
    }

}
