using CoreLib;
using CoreLib.Model;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Stock> stocks = new List<Stock>();
            Scraper scraper = new Scraper();

            foreach (Stock item in stocks)
            {
                try
                {
                    scraper.DownloadData(item.url);
                    string content = scraper.GetStockPrice();
                    var dividends = scraper.GetDividends();
                    Console.WriteLine("{0}: {1}\n{2}", item.ticker, content, string.Join("\n", dividends));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
