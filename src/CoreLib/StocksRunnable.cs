using System;
using System.Threading.Tasks;
using CoreLib.Model;
using Microsoft.Extensions.Configuration;

namespace CoreLib
{
    public class StocksRunnable : IRunnable
    {
        private readonly StockServiceOptions _config;
        private readonly IWebScraper _webScraper;
        private readonly IFileLogger _logger;

        public StocksRunnable(IConfiguration configuration, IWebScraper scraper, IFileLogger logger)
        {
            _config = new StockServiceOptions();
            configuration.GetSection(StockServiceOptions.Key).Bind(_config);
            _webScraper = scraper;
            _logger = logger;
        }

        public async Task<OperationResult> Run()
        {
            var operationResult = new OperationResult();

            foreach (Stock stock in _config.Stocks)
            {
                try
                {
                    _logger.LogInfo($"Downloading data for {stock.Ticker}");
                    await _webScraper.DownloadDataAsync(stock.Url);

                    //example: D:/Workspace/InvestData/stocks/example11
                    var baseFileName = $"{_config.DataDirectory}{stock.Ticker}.txt";
                    var dividendsFile = baseFileName.Replace(".txt", "_dividends.txt");

                    // register current stock price
                    var date = DateTime.Today.ToString("dd/MM/yyyy");
                    string priceStr = _webScraper.GetStockPrice();
                    _logger.AppendToFile($"{date}\t{priceStr}", baseFileName);

                    // register dividends
                    var dividends = _webScraper.GetDividends();
                    if (dividends.Count > 0)
                    {
                        var content = string.Join("\n", dividends);
                        _logger.WriteFile(content, dividendsFile);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception occurred: " + ex.Message);
                    operationResult.Success = false;
                    operationResult.Exceptions.Add(ex);
                }
            }

            return operationResult;
        }
    }
}
