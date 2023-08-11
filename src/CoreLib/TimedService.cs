using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLib
{
    public class TimedService : BackgroundService
    {
        private readonly IRunManager _runManager;
        private readonly IFileLogger _logger;

        public TimedService(IRunManager runManager, IFileLogger logger)
        {
            _runManager = runManager;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await TimerElapsed();
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }

        public async Task TimerElapsed()
        {
            _logger.LogInfo($"Timer elapsed at {DateTime.Now}");
            await _runManager.Run();
        }
    }
}