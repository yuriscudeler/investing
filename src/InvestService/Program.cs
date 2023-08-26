using CoreLib;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "InvestService";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<TimedService>();
        services.AddScoped<IRunManager, DailyRunManager>();
        services.AddScoped<IRunnable, StocksRunnable>();
        services.AddScoped<IWebScraper, WebScraper>();
        services.AddScoped<IFileLogger, FileLogger>();
        services.AddHttpClient();
    })
    .Build();

await host.RunAsync();
