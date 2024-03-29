﻿using CoreLib.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CoreLib
{
    public class DailyRunManager : IRunManager
    {
        private readonly StockServiceOptions _config;
        private DateTime _lastRun;
        private IRunnable _runnable;
        private IFileLogger _logger;

        public DailyRunManager(IConfiguration configuration, IRunnable runnable, IFileLogger logger)
        {
            _config = new StockServiceOptions();
            configuration.GetSection(StockServiceOptions.Key).Bind(_config);
            _runnable = runnable;
            _logger = logger;
        }

        private bool ShouldRun()
        {
            double now = DateTime.Now.TimeOfDay.TotalSeconds;
            double configTime = _config.TimeToRun.TimeOfDay.TotalSeconds;

            return (_lastRun < DateTime.Today) && now >= configTime;
        }

        public async Task Run()
        {
            _logger.LogInfo($"Timer elapsed");

            if (!ShouldRun())
            {
                return;
            }

            var operationResult = await _runnable.Run();

            // only set run if there were no errors
            if (operationResult.Success)
            {
                _lastRun = DateTime.Today;
            }
        }
    }
}
