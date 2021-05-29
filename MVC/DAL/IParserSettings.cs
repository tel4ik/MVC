namespace MVC.DAL
{
    public interface IParserSettings
    {
        string BaseUrl { get; set; }
        string PrefixUrl { get; set; }
        int StartPoint { get; set; }
        int EndPoint { get; set; }

        string FutureAcive { get; set; }
    }
}
