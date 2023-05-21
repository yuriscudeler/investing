using CoreLib;
using CoreLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestService1
{
    internal class StocksService
    {
        private Config config;
        private string dataDirectory = "D:/Workspace/InvestData/";
        private DateTime lastRun;

        public StocksService()
        {
            config = ReadConfig(dataDirectory + "config.json");
        }

        public void TimerElapsed()
        {
            FileIO.WriteLog("Timer elapsed at " + DateTime.Now, dataDirectory);

            if (ShouldRun())
            {
                Run();
            }
        }

        private bool ShouldRun()
        {
            double now = DateTime.Now.TimeOfDay.TotalSeconds;
            double configTime = config.timeToRun.TimeOfDay.TotalSeconds;

            return (lastRun < DateTime.Today) && now >= configTime;
        }

        private void Run()
        {
            bool error = false;
            Scraper scraper = new Scraper();
            foreach (Stock stock in config.stocks)
            {
                try
                {
                    scraper.DownloadData(stock.url);

                    //example: D:/Workspace/InvestData/stocks/example11
                    string baseFileName = string.Format("{0}stocks/{1}", dataDirectory, stock.ticker);

                    // register current stock price
                    string priceStr = scraper.GetStockPrice();
                    FileIO.AppendToFile(string.Format("{0}\t{1}", DateTime.Today.ToString("dd/MM/yyyy"), priceStr), baseFileName + ".txt");

                    // register dividends
                    var dividends = scraper.GetDividends();
                    if (dividends.Count > 0)
                    {
                        FileIO.WriteFile(string.Join("\n", dividends), baseFileName + "_dividends.txt");
                    }
                }
                catch (Exception ex)
                {
                    FileIO.WriteLog("Exception occurred: " + ex.Message, dataDirectory);
                    error = true;
                }
            }

            if (!error)
            {
                lastRun = DateTime.Today;
            }
        }

        // read config file
        private Config ReadConfig(string path)
        {
            if (!File.Exists(path))
            {
                return new Config();
            }

            string fileContent = FileIO.ReadFile(path);
            return Config.FromJson(fileContent);
        }
    }
}
