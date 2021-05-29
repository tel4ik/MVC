using MVC.DAL;
using MVC.DAL.CyberSport;
using MVC.Domain;
using System.Collections.Generic;

namespace MVC.Controller
{
    class MainController
    {
        ParserWorker<List<Competition>> parserList;
        CsPasrseSettings settings;

        private List<Competition> ActiveCompetitions = new List<Competition>();
        private List<Competition> FutureCompetitions = new List<Competition>();
        private List<Competition> LocalMemCompetitions = new List<Competition>();

        ParserWorker<int> CountPagesParser;

        public string date;
        public string url;

        public int StartPoint = 1;
        public int EndPoint;
        
        public void StartParser()
        {
            parserList.Start();
        }

        public List<Competition> GetCompetitions
        {
            get => ActiveCompetitions;
            set => ActiveCompetitions = value;
        }

        public List<Competition> GetFutureCompetitions()
        {
            return FutureCompetitions;
        }

        public List<Competition> LocCompetitions
        {
            set { LocalMemCompetitions = value; }
            get { return LocalMemCompetitions; }
        }
        public void settingsParser(IParserSettings settings, string func)
        {
            parserList.ParserSettings = settings;
            if (func == "today")
            {
                parserList.OnNewData -= ParserOnComplitedFuture;
                parserList.OnNewData += ParserOnComplitedActive;
            }
        }
        public void InitParser()
        {
            parserList = new ParserWorker<List<Competition>>(new CsParser());
            parserList.OnNewData += ParserOnComplitedFuture;
            //parserList.OnCompleted += ParserComplited;
            //parserList.OnNewData += ParserOnComplitedFuture;
        }

        private void ParserOnComplitedActive(object arg, List<Competition> args)
        {
            ActiveCompetitions.AddRange(args);
        }
        private void ParserOnComplitedFuture(object arg, List<Competition> args)
        {
            FutureCompetitions.AddRange(args);
        }
        public string GetBaseUrl
        {
            get => "https://www.cybersport.ru";
        }

        public void CountPage(IParserSettings setin)
        {
            CountPagesParser = new ParserWorker<int>(new EndsParser(), setin);
            CountPagesParser.ParserSettings = setin;
            CountPagesParser.OnNewData += ParserOnComplitedPage;
            CountPagesParser.Start();
        }

        private void ParserOnComplitedPage(object arg, int args)
        {
            EndPoint = args;
        }
        public int GetEndPoint { get => EndPoint; }
    }
}