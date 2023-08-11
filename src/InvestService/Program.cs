using CoreLib;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "InvestService";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<TimedService>();
        services.AddScoped<IWebScraper, WebScraper>();
        services.AddHttpClient();
    })
    .Build();

await host.RunAsync();
