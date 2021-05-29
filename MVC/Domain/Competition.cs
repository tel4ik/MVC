using System.Collections.Generic;

namespace MVC.Domain
{
    public class Competition
    {
        public List<string> EventsTournament = new List<string>();
        public string TournamentName { get; set; }
        public string Date { get; set; }
        public string[] Commands { get; set; }
        public string NameOfGame { get; set; }
        public string url { get; set; }
        public override string ToString()
        {
            return TournamentName;
        }
    }
}
