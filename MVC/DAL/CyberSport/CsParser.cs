using AngleSharp.Html.Dom;
using MVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MVC.DAL.CyberSport
{
    class CsParser : IParser<List<Competition>>
    {
        public List<Competition> Parse(IHtmlDocument document)
        {
            var Competitions = new List<Competition>();
            

            var items = document.QuerySelectorAll("li").Where(item => item.ClassName != null && item.ClassName.Contains("matches__item") && item.ClassName.Length < 15).ToList();

            if (items.Count() == 0)
            {
                return Competitions;
            }
            else
            {
                foreach (var info in items)
                {
                    var _competition = new Competition();
                    _competition.TournamentName = info.QuerySelector("span").TextContent;
                    _competition.url = info.QuerySelector("a").GetAttribute("href").ToString();
                    
                    string temp = info.QuerySelector("i").ClassName.ToString();
                    string buf = "";
                    _competition.Date = info.QuerySelector("time").TextContent;
                    for (int i = temp.Count() - 1; i > 0; i--)
                    {
                        if (temp[i] == '-' && temp[i - 1] == '-') break;
                        buf += temp[i];
                    }
                    string output = "";
                    for (int i = buf.Length - 1; i >= 0; i--)
                    {
                        output += buf[i];
                    }

                    _competition.NameOfGame = output.Replace("-", " ");

                    var names = info.QuerySelectorAll("span").Where(item => item.ClassName != null && item.ClassName == "d--inline-block d--phone-none").Select(item => item.TextContent).ToArray<string>();
                    if (names.Length == 0) names = info.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName == "team__name").Select(item => item.TextContent).ToArray<string>();
                    _competition.Commands = names;
                    
                    Competitions.Add(_competition);
                }
            }

            return Competitions;
        }
    }
}
