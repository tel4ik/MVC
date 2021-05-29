namespace MVC.DAL.CyberSport
{
    class CsPasrseSettings : IParserSettings
    {
        public CsPasrseSettings(int start, int end, string middlePrefix)
        {
            StartPoint = start;
            EndPoint = end;
            FutureAcive = middlePrefix;
        }

        public string BaseUrl { get; set; } = "https://www.cybersport.ru";
        public string PrefixUrl { get; set; } = "base/match?status=active&page=CurrentId";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public string FutureAcive { get; set; } 
    }
}
