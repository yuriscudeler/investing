using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLib
{
    public class TimedService : BackgroundService
    {
        private readonly IRunManager _runManager;

        public TimedService(IServiceScopeFactory scopeFactory)
        {
            using var scope = scopeFactory.CreateScope();
            _runManager = scope.ServiceProvider.GetService<IRunManager>();
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
            await _runManager.Run();
        }
    }
}