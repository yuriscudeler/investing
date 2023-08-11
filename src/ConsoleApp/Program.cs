using CoreLib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();
services.AddScoped<IWebScraper, WebScraper>();
services.AddScoped<TimedService>();
services.AddScoped<IFileLogger, FileLogger>();
services.AddScoped<IRunManager, DailyRunManager>();
services.AddScoped<IRunnable, StocksScraping>();
services.AddHttpClient();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

services.AddSingleton(config);

var provider = services.BuildServiceProvider();
var service = provider.GetRequiredService<TimedService>();

await service.TimerElapsed();

Console.WriteLine("Done!");
