using BackgroundWorker;
using CoreLib;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<IRunManager, DailyRunManager>();
builder.Services.AddScoped<IRunnable, StocksRunnable>();
builder.Services.AddScoped<IWebScraper, WebScraper>();
builder.Services.AddScoped<IFileLogger, FileLogger>();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<TimedService>();
// builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
