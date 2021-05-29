using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC.DAL
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}/{settings.PrefixUrl.Replace("active", settings.FutureAcive)}";
        }

        public async Task<string> GetSourceByPage(int id)
        {
            var currentUrl = url.Replace("CurrentId", id.ToString());
            var response = await client.GetAsync(currentUrl);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
