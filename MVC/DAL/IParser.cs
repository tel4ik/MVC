using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace MVC.DAL
{
    public interface IParser<T>
    {
        T Parse(IHtmlDocument document);
        
    }
}
