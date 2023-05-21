using CoreLib;

namespace InvestService1
{
    public class ServiceBase : BackgroundService
    {
        private StocksService _stocksService;

        public ServiceBase()
        {
            _stocksService = new StocksService();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _stocksService.TimerElapsed();
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }
}