using CoreLib.Model;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CoreLib
{
    public interface IWebScraper
    {
        Task DownloadDataAsync(string url);
        string GetStockPrice();
        List<Dividend> GetDividends();
    }

    public class WebScraper : IWebScraper
    {
        public Stream currentData;
        public HtmlDocument htmlDoc;
        public IHttpClientFactory _httpClientFactory;

        public WebScraper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task DownloadDataAsync(string url)
        {
            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            client.DefaultRequestHeaders.Add("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            var response = await client.GetAsync(url);
            currentData = await response.Content.ReadAsStreamAsync();
            htmlDoc = new HtmlDocument();
            htmlDoc.Load(currentData);
        }

        public string GetStockPrice()
        {
            var nodes = htmlDoc.DocumentNode.SelectNodes("//strong[@class='value']");
            return nodes[0].InnerText;
        }

        public List<Dividend> GetDividends()
        {
            List<Dividend> list = new List<Dividend>();
            HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("//input[@id='results']");

            if (node == null)
            {
                return list;
            }

            string value = node.GetAttributeValue("value", string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                list.AddRange(JsonConvert.DeserializeObject<List<Dividend>>(HttpUtility.HtmlDecode(value)));
            }

            return list;
        }
    }
}
