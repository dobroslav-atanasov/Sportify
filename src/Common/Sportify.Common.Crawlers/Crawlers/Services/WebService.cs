namespace Sportify.Common.Crawlers.Crawlers.Services
{
    using System.IO;
    using System.Net;

    public class WebService
    {
        public string GetRequest(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (var response = (HttpWebResponse) request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var html = reader.ReadToEnd();
                return html;
            }
        }
    }
}