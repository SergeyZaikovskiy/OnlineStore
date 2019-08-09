using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        //Значение текущего свойства
        public string Property { get; set; }
        //Значение активного свойства
        public string Current { get; set; }
        //Действие контроллера
        public string ActionName { get; set; }
        //Название контроллера
        public string ControllerName { get; set; }
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
            string url = urlHelper.Action(ActionName, ControllerName,  new { sortValue = Property, Area = AreaName });
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
