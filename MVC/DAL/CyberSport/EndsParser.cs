using AngleSharp.Html.Dom;
using MVC.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MVC.DAL.CyberSport
{
    class EndsParser : IParser<int>
    {
        public int Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("pagination__item")).Count();
            if (items == 0) return 1;
            return items - 1;
        }
    }
}
