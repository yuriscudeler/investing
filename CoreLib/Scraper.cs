using CoreLib.Model;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;

namespace CoreLib
{
    public class Scraper
    {
        public byte[] currentData;
        public HtmlDocument htmlDoc;

        public Scraper()
        {
            
        }

        public void DownloadData(string url)
        {
            // TODO: replace by HttpClient
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36");
                client.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                client.Headers.Add(HttpRequestHeader.AcceptLanguage, "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                currentData = client.DownloadData(url);
                htmlDoc = new HtmlDocument();
                htmlDoc.Load(new MemoryStream(currentData));
            }
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
