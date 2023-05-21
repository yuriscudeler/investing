using InvestService1;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "InvestService";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<ServiceBase>();
    })
    .Build();

await host.RunAsync();
