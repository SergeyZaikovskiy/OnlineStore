using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineStore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.TagHelpers
{
    /// <summary>
    /// Тагхелпер для пагинации данных
    /// </summary>
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        //Область контроллера
        public string AreaName { get; set; }
        //Действие контроллера
        public string ActionName { get; set; }
        //Название контроллера
        public string ControllerName { get; set; }        
        //Данные для передачи в контроллер
        public Dictionary<string, string> RouteData { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            // набор ссылок будет представлять список ul
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");



            if (PageModel.TotalPages>0)
            {            
                // формируем три ссылки - на текущую, предыдущую и следующую
                TagBuilder currentItem = CreateTag(PageModel.PageNumber, urlHelper);

                //сслыка на страницу на 2 меньше чем последняя, если мы находимся на последней странице
                if (PageModel.PageNumber == PageModel.TotalPages && PageModel.TotalPages > 2)
                {
                    TagBuilder prevItem = CreateTag(PageModel.PageNumber - 2, urlHelper);
                    tag.InnerHtml.AppendHtml(prevItem);
                }

                // создаем ссылку на предыдущую страницу, если она есть
                if (PageModel.HasPreviousPage)
                {
                    TagBuilder prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper);
                    tag.InnerHtml.AppendHtml(prevItem);
                }

                tag.InnerHtml.AppendHtml(currentItem);
                // создаем ссылку на следующую страницу, если она есть
                if (PageModel.HasNextPage)
                {
                    TagBuilder nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper);
                    tag.InnerHtml.AppendHtml(nextItem);
                }
           
                //сслыка на страницу на 2 больше чем первая, если мы находимся на первой странице
                if (PageModel.PageNumber == 1 && PageModel.TotalPages > 2)
                {
                    TagBuilder prevItem = CreateTag(PageModel.PageNumber + 2, urlHelper);
                    tag.InnerHtml.AppendHtml(prevItem);
                }
            }

            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");

            if(RouteData.ContainsKey("page"))
                RouteData["page"] =  pageNumber.ToString();
            else
                RouteData.Add("page", pageNumber.ToString());

            
            if (pageNumber == this.PageModel.PageNumber)
            {
                item.AddCssClass("active");
            }
            else
            {
                var url = CheckDataRoute(RouteData);
                link.Attributes["href"] = CreateURL(url);
            }           

            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }

        private string CheckDataRoute(Dictionary<string, string> DataDictionary)
        {
            var fixedRouteValues = new Dictionary<string, string>();
            foreach (var (newKey, value) in DataDictionary.Where(r => !string.IsNullOrWhiteSpace(r.Value)))
            {
                var key = fixedRouteValues.Keys.FirstOrDefault(k => string.Equals(k, newKey, StringComparison.InvariantCultureIgnoreCase)) ?? newKey;
                fixedRouteValues[key] = value;
            }            
            var query = string.Join("&", fixedRouteValues.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            return query;
        }//проверка данных в словаре для машрутизации и передачи данных в контроллер, чтобы не попадались пустые

        private string CreateURL(string dataString)
        {
            string url;
            if (AreaName == null)
                url = "/" + ControllerName + "/" + ActionName + "?" + dataString;
            else
                url = "/" + AreaName + "/" + ControllerName + "/" + ActionName + "?" + dataString;

            return url;
        }
    }
}

