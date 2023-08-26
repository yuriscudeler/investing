using CoreLib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();
services.AddScoped<TimedService>();
services.AddScoped<IRunManager, DailyRunManager>();
services.AddScoped<IRunnable, StocksRunnable>();
services.AddScoped<IWebScraper, WebScraper>();
services.AddScoped<IFileLogger, FileLogger>();
services.AddHttpClient();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

services.AddSingleton(config);

var provider = services.BuildServiceProvider();
var service = provider.GetRequiredService<TimedService>();

await service.TimerElapsed();

Console.WriteLine("Done!");
